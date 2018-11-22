using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
	private float baseRange;
	public float rangeVariance = 0.0f;

	private float baseIntensity;
	public float intensityVariance = 0.0f;

	[Tooltip("Leave it at 0 for one change per frame")]
	public float changeDelay = 0.0f;
	private float timer;

	private new Light light;

	// Use this for initialization
	void Start ()
	{
		light = GetComponent<Light>();

		baseRange = light.range;
		baseIntensity = light.intensity;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer -= Time.deltaTime;

		if (timer <= 0.0f)
		{
			light.range = baseRange + (rangeVariance * Random.Range(-1.0f, 1.0f));
			light.intensity = baseIntensity + (intensityVariance * Random.Range(-1.0f, 1.0f));

			timer = changeDelay;
		}
	}
}
