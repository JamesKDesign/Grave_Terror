using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProximitySensor : MonoBehaviour
{
	private Enemy enemy;
	public List<Enemy> nearby = new List<Enemy>(20);
	private void Start()
	{
		enemy = GetComponentInParent<Enemy>();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
			nearby.Add(other.gameObject.GetComponent<Enemy>());
		else if (other.CompareTag("Player"))
		{
			if (other.GetComponent<PlayerHealth>().playerState == PlayerHealth.PlayerState.ALIVE)
			{
				enemy.engaging = true;
				enemy.target = other.gameObject;
			}
		}
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player") && enemy.target != null)
		{
			if (other.GetComponent<PlayerHealth>().playerState == PlayerHealth.PlayerState.ALIVE)
			{
				enemy.engaging = true;
				enemy.target = other.gameObject;
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Enemy"))
			nearby.Remove(other.gameObject.GetComponent<Enemy>());
		else if (other.CompareTag("Player"))
		{
			enemy.engaging = false;
			enemy.target = null;
		}
	}
	public void EntityDied(Enemy _enemy)
	{
		nearby.Remove(_enemy);
	}
}
