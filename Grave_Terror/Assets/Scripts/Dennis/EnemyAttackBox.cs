using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{

	Enemy parent;
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
		if(target.GetComponent<PlayerHealth>().playerState == PlayerHealth.PlayerState.DEAD)
		{
			parent.engaging = false;
			target = null;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && target.GetComponent<PlayerHealth>().playerState != PlayerHealth.PlayerState.DEAD)
		{
			target = other.gameObject;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player") && target.GetComponent<PlayerHealth>().playerState != PlayerHealth.PlayerState.DEAD)
		{
			parent.AttackSignal();
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
