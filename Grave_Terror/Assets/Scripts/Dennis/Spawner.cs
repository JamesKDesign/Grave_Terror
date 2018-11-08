using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[System.Serializable]
	public class EnemySpawn
	{
		[HideInInspector]
		public bool triggered = false; //internal value
		[Tooltip("Dependent on the order of Enemy Prefab, Use the element ID")]
		public int enemyID;
		[Tooltip("How many of this enemy to spawn")]
		public int amount;
		[Tooltip("Should they spawn from random spawnpoints?")]
		public bool randomSpawn;
		[Tooltip("If they are spawning at random spawnpoints, should they only spawn near the player?")]
		public bool nearbySpawnOnly; //Relative to the players average position
		[Tooltip("How close should the spawner be to the player if spawning only nearby?")]
		public float nearThreshold; //How close should the spawners be?
		[Tooltip("What spawn these enemies will spawn from\nIgnored if random spawner is true")]
		public int spawn; //If not a random spawner, it will be this one
		[Tooltip("How long between enemy spawns")]
		public float delayBetweenSpawns; //How long until the next enemy appears
		[Tooltip("Time that this spawner remains inactive")]
		public float delayUntilActive;
		//public bool trigger; Possible to activate this spawn via world trigger
		[Tooltip("Does this event loop?")]
		public bool loop; //Loop forever, uses increased delay for repeat
	}

	//Internal class for queuing up spawns
	public class EventSpawn
	{
		public float delay;
		public int enemyID;
		public int spawn;
		public bool randomSpawn;
		public bool nearbySpawnOnly;
		public float nearThreshold;

		public float time;
		public int remainingSpawns;
	}
	//The player(s)
	public GameObject m_player;
	//Places enemies should spawn from
	public Transform[] m_spawnPoints;
	//Different enemy prefabs, Order is important
	public GameObject[] m_enemyPrefab;
	//Spawn data
	public EnemySpawn[] m_enemySpawning;
	//Pool for enemies that are ready to be used or are already active
	public int m_poolSize;
	public Stack<GameObject> m_enemyReady = new Stack<GameObject>();
	//public List<GameObject> m_enemyActive = new List<GameObject>();
	//Event for delayed spawn sequences
	private List<EventSpawn> m_eventSpawn = new List<EventSpawn>();
	//Timer
	private float m_timer = 0.0f;

	private void Start()
	{
		//Setup the object pool
		for (int i = 0; i < m_poolSize; ++i)
		{
			GameObject obj = Instantiate<GameObject>(m_enemyPrefab[0], transform);
			//GameObject obj = new GameObject();
			//Enemy enemy = obj.AddComponent<Enemy>();
			Enemy enemy = obj.GetComponent<Enemy>();
			
			//obj.AddComponent<MeshRenderer>();
			//obj.AddComponent<MeshFilter>();
			//Reference back to this so enemies can tell us about their demise
			enemy.spawner = this;

			//obj.transform.parent = transform;

			m_enemyReady.Push(obj);

			obj.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update()
	{
		//Spawning BGN
		m_timer += Time.deltaTime;
		//Check for any spawn events that need be get triggered
		for (int i = 0; i < m_enemySpawning.Length; ++i)
		{
			//Ignore any that have already been triggered
			if (m_enemySpawning[i].triggered)
				continue;
			//Untriggered spawn that has to be triggered NOW
			if (m_enemySpawning[i].delayUntilActive < m_timer)
			{
				if (m_enemySpawning[i].loop)
				{
					m_enemySpawning[i].delayUntilActive += m_enemySpawning[i].delayUntilActive;
				}
				else
				{
					m_enemySpawning[i].triggered = true;
				}
				//Go
				Parse(i);
			}
		}
		//Loop for any in progress spawnings
		for (int i = 0; i < m_eventSpawn.Count; ++i)
		{
			m_eventSpawn[i].time += Time.deltaTime;
			//Spawn something everytime the delay has passed
			if (m_eventSpawn[i].time >= m_eventSpawn[i].delay)
			{
				m_eventSpawn[i].time -= m_eventSpawn[i].delay;
				//Countdown till event complete
				m_eventSpawn[i].remainingSpawns--;

				//If it has to spawn somewhere random do so
				if (m_eventSpawn[i].randomSpawn)
				{
					int spawn = (int)Random.Range(0, (float)m_spawnPoints.Length - 0.1f);
					Spawn(m_eventSpawn[i].enemyID, 1, spawn);
				}
				else //Fixed spawn point
				{
					Spawn(m_eventSpawn[i].enemyID, 1, m_eventSpawn[i].spawn);
				}

				//We have spawned all that we should for this event... remove it from the list
				if (m_eventSpawn[i].remainingSpawns == 0)
				{
					m_eventSpawn.RemoveAt(i);
					i--; //Decrease counter to stop counting over
				}
			}
		}
		//Spawning END
	}

	//Internal Function - Figure out if to spawn them now or add them to an event list
	private void Parse(int _index)
	{
		//Log any attempts to spawn to much stuff
		if (m_enemySpawning[_index].amount > m_enemyReady.Count)
		{
			Debug.Log("You are trying to spawn " + m_enemySpawning[_index].amount + " but you only have " + m_enemyReady.Count + " left\nTrying anyway...");
		}
		//Enemies should all be spawned instantly, there is no need to use the event queue
		if (m_enemySpawning[_index].delayBetweenSpawns == 0.0f)
		{
			//If it has to spawn somewhere random do so
			if (m_enemySpawning[_index].randomSpawn)
			{
				int spawn = (int)Random.Range(0, (float)m_spawnPoints.Length - 0.1f);
				Spawn(m_enemySpawning[_index].enemyID, m_enemySpawning[_index].amount, spawn);
			}
			else //Fixed spawn point
			{
				Spawn(m_enemySpawning[_index].enemyID, m_enemySpawning[_index].amount, m_enemySpawning[_index].spawn);
			}
		}
		else
		{
			//Just copy data over
			EventSpawn es = new EventSpawn();
			es.delay = m_enemySpawning[_index].delayBetweenSpawns;
			es.enemyID = m_enemySpawning[_index].enemyID;
			es.spawn = m_enemySpawning[_index].spawn;
			es.randomSpawn = m_enemySpawning[_index].randomSpawn;
			es.nearbySpawnOnly = m_enemySpawning[_index].nearbySpawnOnly;
			es.nearThreshold = m_enemySpawning[_index].nearThreshold;
			es.remainingSpawns = m_enemySpawning[_index].amount;
			es.time = 0.0f;
			//Add it to the list of spawn events to be processed
			m_eventSpawn.Add(es);
		}
	}

	//Internal function - Spawning enemies
	private void Spawn(int _enemy_ID, int _amount, int _spawn)
	{
		//Loop creation until _amount have been made
		for (int i = 0; i < _amount; ++i)
		{
			//Nothing left to spawn
			if (m_enemyReady.Count == 0)
			{
				Debug.Log("Error: No entities remaining in pool");
				return;
			}
			//Spawn it
			GameObject obj = m_enemyReady.Pop();
			obj.transform.parent = null;

			obj.transform.position = m_spawnPoints[_spawn].position;

			obj.SetActive(true);

			obj.GetComponent<Enemy>().Init();
		}
	}

	//Should be called by any object spawned by this class when it expires
	public void Despawn(GameObject _enemy)
	{
		m_enemyReady.Push(_enemy);

		_enemy.transform.parent = transform;

		_enemy.SetActive(false);
	}

	//Allow other scripts to spawn enemies at a location of their choosing
	public void RemoteSpawn(Vector3 _position)
	{
		if (m_enemyReady.Count == 0)
		{
			Debug.Log("Error: No entities remaining in pool\nSpawn requested from remote at: " + _position.ToString());
			return;
		}

		GameObject obj = m_enemyReady.Pop();
		obj.transform.parent = null;

		obj.transform.position = _position;

		obj.SetActive(true);

		obj.GetComponent<Enemy>().Init();
	}
}
