using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserPointer : MonoBehaviour
{
	LineRenderer line;
	private void Start()
	{
		line = GetComponent<LineRenderer>();
		line.useWorldSpace = false;
	}
	// Update is called once per frame
	void Update ()
	{
		float distance = 100.0f;
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.forward, out hit, 100.0f, LayerMask.GetMask(new string[]{ "Enemy","Default"})))
		{
			distance = hit.distance;
		}
		line.SetPosition(1, new Vector3(0.0f, 0.0f, distance));
	}
}
