using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleSpawner : MonoBehaviour
{
	private Spawner spawner = null;

	public float spawnDelay;
	public float minRadius;
	public float maxRadius;

	public bool active;

	[Tooltip("Limit spawn amount")]
	public int maxSpawns = int.MaxValue;

	private float timer;
	// Use this for initialization
	void Start ()
	{
		spawner = Spawner.instance;

		timer = spawnDelay;
	}
	
	// Update is called once per frame
	void Update ()
	{
        timer -= Time.deltaTime;
		if (active && timer <= 0.0f && maxSpawns > 0)
		{
			Vector2 radius = (Random.insideUnitCircle * maxRadius) + new Vector2(minRadius, minRadius);
			Vector3 region = new Vector3(radius.x, 0.0f, radius.y);
			//Spawn one
			spawner.RemoteSpawn(region + transform.position);
			//Reset timer
			timer = spawnDelay;
			//Decrease limit
			maxSpawns--;
		}
	}

	public void Trigger()
	{
		active = true;
	}
}
