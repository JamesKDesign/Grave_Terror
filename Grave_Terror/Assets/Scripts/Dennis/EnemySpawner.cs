using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A simpler, cleaner spawner script
public class EnemySpawner : MonoBehaviour
{
	[System.Serializable]
	public struct WaveData
	{
		public int slowEnemyCount;
		public int fastEnemyCount;
		public float slowSpawnDelay;
		public float fastSpawnDelay;
	}
	//Prefabs for the enemies we are going to spawn
	public GameObject slowEnemyPrefab;
	public GameObject fastEnemyPrefab;

	//Wave 1... 2... 3... 
	public WaveData[] waves;

	//Variables to see how far we've progressed
	private int slowsSpawned = 0;
	private int fastsSpawned = 0;
	private float timer1 = 0.0f;
	private float timer2 = 0.0f;

	//What wave are we on
	[HideInInspector]
	public int wave = 0;
	private bool waveInProgress = false;

	//Between wave
	[Tooltip("How long until the next wave")]
	public float nextWaveDelay;
	[Tooltip("Should we wait for all enemies in the current wave to die before countdown to next wave?")]
	public bool waitForAllClear;


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer1 += Time.deltaTime;
		timer2 += Time.deltaTime;
		//Check if the wave is still spawning stuff
		if (waveInProgress)
		{
			if (timer1 >= waves[wave].slowSpawnDelay)
			{
				timer1 -= waves[wave].slowSpawnDelay;
				//Spawn a slow
			}
			if (timer2 >= waves[wave].fastSpawnDelay)
			{
				timer2 -= waves[wave].fastSpawnDelay;
				//Spawn a fast
			}
			//have we spawned all that we can?
			if (slowsSpawned == waves[wave].slowEnemyCount && fastsSpawned == waves[wave].fastEnemyCount)
			{
				waveInProgress = false;
				//Reset a bunch of variables
				slowsSpawned = 0;
				fastsSpawned = 0;
				timer1 = 0.0f;
				timer2 = 0.0f;
			}
		}
		else
		{



			//Next wave
			if (timer1 >= nextWaveDelay)
			{
				wave++;
				//If we 
				if (wave == waves.Length)
				{
					enabled = false;
					return;
				}

				timer1 = 0.0f;
				timer2 = 0.0f;

				waveInProgress = true;
			}
		}
	}
}
