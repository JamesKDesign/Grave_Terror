using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	#region _OLD_STUFF_
	////Class to get enemies in groups before they aggresively pursue the player
	//public class Horde
	//{
	//	public enum HORDESTATE
	//	{
	//		IDLE,
	//		PATHING,
	//		PURSUE
	//	}
	//	public Enemy leader = null;
	//	public List<Enemy> members;
	//	public NavMeshPath leaderPath = null;
	//	//Pursuing hordes will not be given new members
	//	public HORDESTATE state = HORDESTATE.IDLE;

	//	public float timer = 0.0f;

	//	//Selects a new actor to be assigned leader of the horde
	//	public void SelectLeader()
	//	{
	//		//Get the average 2D position of all members
	//		Vector2 center = Vector2.zero;
	//		for (int i = 0; i < members.Count; ++i)
	//		{
	//			center += new Vector2(members[i].transform.position.x, members[i].transform.position.z);
	//		}
	//		//Find out which member is the closest to the average center
	//		Enemy closest = null;
	//		float closestDistance = Mathf.Infinity;
	//		for (int i = 0; i < members.Count; ++i)
	//		{
	//			//Squared magnitude because its faster
	//			float distance = Vector2.SqrMagnitude(center - new Vector2(members[i].transform.position.x, members[i].transform.position.z));
	//			if (distance < closestDistance)
	//			{
	//				closest = members[i];
	//				closestDistance = distance;
	//			}
	//		}
	//		//The closest to the center becomes the leader
	//		leader = closest;
	//	}

	//	void AddMember(Enemy _member)
	//	{
	//		//if we dont already have him in our horde, add him
	//		if (!members.Contains(_member))
	//		{
	//			members.Add(_member);
	//		}
	//	}

	//	//Removes an actor from the horde
	//	public void RemoveMember(Enemy _member)
	//	{
	//		//Remove him from this horde
	//		members.Remove(_member);
	//		//If he was the leader, select a new one
	//		if (_member == leader)
	//		{
	//			SelectLeader();
	//		}
	//	}

	//	//Set the object that the leader will pursue
	//	public void SetTarget(GameObject _target)
	//	{
	//		leader.target = _target;

	//	}
	//}
	#endregion

	[HideInInspector]
	public static EnemyController instance = null;

	//Reference to the players
	public GameObject player1;
	public GameObject player2;

	//The collective to keep track off
	private List<Enemy> enemies = new List<Enemy>(20);

	[Tooltip("Whats layers Actors should check against to see if they have line of sight on the player")]
	public LayerMask checkLayers;

	//Time to refresh the pathing of enroute agents
	private float refreshTimer;

	// Use this for initialization
	void Awake()
	{
		//There should only be one instance of this
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Debug.Log("Multiple EnemyControllers Detected! Ignoring copies");
			Destroy(this);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		refreshTimer += Time.deltaTime;
		//The average of the players positions
		Vector3 averagePos = (player1.transform.position + player2.transform.position) / 2.0f;
		//Hordes
		foreach (Enemy e in enemies)
		{
			switch(e.state)
			{
				case E_STATE.IDLE:
					{
						//Create a pathing behaviour for the leader
						EnemyPathing pathing = new EnemyPathing(e);
						pathing.weight = 1.0f;

						//assign the behaviour to the actor
						e.behaviours.Add(pathing);

						e.path = new NavMeshPath();

						//Calculate a path to that location
						NavMesh.CalculatePath(e.transform.position, averagePos, NavMesh.AllAreas, e.path);

						e.state = E_STATE.PATHING;
					}
				break;
				//Pathing to the player
				case E_STATE.PATHING:
					{
						//Alt method needed
						if (refreshTimer >= 1.0f)
						{
							NavMesh.CalculatePath(e.transform.position, averagePos, NavMesh.AllAreas, e.path);
							e.ResetBehaviours();
						}

						//Raycast from the leader towards both players
						NavMeshHit hit;
						if (!NavMesh.Raycast(e.transform.position, player1.transform.position, out hit, NavMesh.AllAreas))
						{
							e.state = E_STATE.ENGAGING;
							e.target = player1;
							//Temp weight change
							e.behaviours.RemoveAt(0);
							//e.behaviours.Add(new EnemyPursue(e));
							//Temp end
							break;
						}
						//2nd player
						if (!NavMesh.Raycast(e.transform.position, player2.transform.position, out hit, NavMesh.AllAreas))
						{
							e.state = E_STATE.ENGAGING;
							e.target = player2;
							//Temp weight change
							e.behaviours.RemoveAt(0);
							//e.behaviours.Add(new EnemyPursue(e));
							//Temp end
							break;
						}
					}
					break;
				//Directly heading to player
				case E_STATE.ENGAGING:
					{
						NavMeshHit hit;
						//If theres an obstruction go back to pathfinding
						if (NavMesh.Raycast(e.transform.position, e.target.transform.position, out hit, NavMesh.AllAreas))
						{
							e.state = E_STATE.PATHING;
							e.ResetBehaviours();
							e.target = null;
							//Temp weight change
							e.behaviours.Add(new EnemyPathing(e));
							e.behaviours.RemoveAt(0);
							//Temp end
							break;
						}
					}
					break;
			}
		}
		if (refreshTimer >= 1.0f)
		{
			refreshTimer = 0.0f;
		}
	}

	//Join the hivemind
	public void RegisterEnemy(Enemy _enemy)
	{
		//Add the enemy to the collective
		enemies.Add(_enemy);
	}

	//A member of the collective has died
	public void DeregisterEnemy(Enemy _enemy)
	{
		//Remove the enemy from the collective
		enemies.Remove(_enemy);
	}

	public void DispatchAll()
	{
		foreach (Enemy e in enemies)
			e.state = E_STATE.ENGAGING;
	}
}
