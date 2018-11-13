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
			Vector3 radius = new Vector3(Random.Range(-maxRadius, maxRadius), 0.0f, Random.Range(-maxRadius, maxRadius));
			if (radius.sqrMagnitude < minRadius * minRadius)
			{
				float sub = maxRadius - minRadius;
				radius.x += minRadius;
				radius.y += minRadius;
			}
			spawner.RemoteSpawn(radius + transform.position);
			timer = spawnDelay;
		}
	}
}
