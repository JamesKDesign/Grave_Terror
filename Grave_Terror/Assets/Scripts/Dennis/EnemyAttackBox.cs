using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{

	Enemy parent;
	[HideInInspector]
	public GameObject target = null;

	void Awake ()
	{
		//Forceably rename the object i'm attached to too AttackBox
		gameObject.name = "AttackBox";
	}

	void Start ()
	{
		parent = GetComponentInParent<Enemy>();
	}

	private void Update()
	{
		if (target == null)
			return;
		if(target.GetComponent<PlayerHealth>().playerState != PlayerHealth.PlayerState.ALIVE)
		{
			parent.engaging = false;
			target = null;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && other.GetComponent<PlayerHealth>().playerState == PlayerHealth.PlayerState.ALIVE)
		{
			target = other.gameObject;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player") && other.GetComponent<PlayerHealth>().playerState == PlayerHealth.PlayerState.ALIVE)
		{
			target = other.gameObject;
			parent.AttackSignal();
			parent.engaging = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			target = null;
		}
	}
}
