using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	// TESTING PURPOSES ONLY //

    public Transform target;
	NavMeshAgent enemy;
	public float speed = 10f;
    public float damage;
	

	private void Awake() {
		target = GameObject.FindGameObjectWithTag("Player").transform;
		enemy = GetComponent<NavMeshAgent>();
	}

	private void Update() {
		enemy.SetDestination(target.position);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            other.transform.GetComponent<PlayerHealth>().RemoveHealth(damage);
        }
        else
        {
            Debug.Log("Collision not working");
        }
    }
}