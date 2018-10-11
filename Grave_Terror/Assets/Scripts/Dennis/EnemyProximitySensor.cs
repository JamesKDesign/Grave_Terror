using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProximitySensor : MonoBehaviour
{

	private void OnTriggerEnter(Collider other)
	{
		//add the object that entered to a list if it is an enemy
	}
	private void OnTriggerExit(Collider other)
	{
		//remove the object from the list if it is an enemy
	}
}
