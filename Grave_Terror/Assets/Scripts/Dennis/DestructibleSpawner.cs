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

	private AudioSource audioSource;
	public AudioClip coffinOpen;
	public float minPitch = 0.95f;
	public float maxPitch = 1.05f;

	public float minDelay = 0.0f;
	public float maxDelay = 1.0f;

	private float timer;
	// Use this for initialization
	void Start ()
	{
		spawner = Spawner.instance;

		timer = spawnDelay;

		audioSource = GetComponent<AudioSource>();
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

		audioSource.pitch = Random.Range(minPitch, maxPitch);
//		audioSource.PlayDelayed = Random.Range(minDelay, maxDelay);
		audioSource.PlayOneShot(coffinOpen);
	}
}
