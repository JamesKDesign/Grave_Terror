using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProximitySensor : MonoBehaviour
{

	public List<Enemy> nearby = new List<Enemy>(20);

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
			nearby.Add(other.gameObject.GetComponent<Enemy>());
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Enemy"))
			nearby.Remove(other.gameObject.GetComponent<Enemy>());
	}
	public void EntityDied(Enemy _enemy)
	{
		nearby.Remove(_enemy);
	}
}
