using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tether : MonoBehaviour
{
	[Tooltip("Other gameobject to tether too")]
	public Tether link;

	public float maxDistance;

	private Vector3 last;

	private Vector3 linkLast;

	// Use this for initialization
	void Start ()
	{
		//If we arent tethered to something just fade away
		if (link == null)
		{
			//Debug.Log(gameObject.name + " has no tether link");
			Destroy(this);
		}
		//If link distances are different just average both
		if (link.maxDistance != maxDistance)
		{
			//Debug.Log("Tether length synced");
			maxDistance = maxDistance + link.maxDistance / 2.0f;
			link.maxDistance = maxDistance;
		}
		//Debug.Log(gameObject.name + " tethered too " + link.gameObject.name);

		last = transform.position;
		linkLast = link.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Vector3.Distance(transform.position, linkLast) > maxDistance)
		{
			transform.position = -Vector3.ClampMagnitude(linkLast - transform.position, maxDistance) + linkLast;
		}

		last = transform.position;
		//Tell our tethered link where we are now
		link.SendPosition(last);
	}

	private void SendPosition(Vector3 _pos)
	{
		linkLast = _pos;
	}
}
