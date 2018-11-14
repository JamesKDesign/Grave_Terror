using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleSpawner : MonoBehaviour
{
	private Spawner spawner = null;

	public float spawnDelay;
	public float minRadius;
	public float maxRadius;

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
		if (timer <= 0.0f)
		{
			Vector2 radius = Random.insideUnitCircle * maxRadius + new Vector2(minRadius, minRadius);
			Vector3 region = new Vector3(radius.x, 0.0f, radius.y);

			spawner.RemoteSpawn(region + transform.position);
			timer = spawnDelay;
		}
	}
}
