using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public Transform target; 
	NavMeshAgent enemy;
	public float speed = 10f;
	

	private void Awake() {
		target = GameObject.FindGameObjectWithTag("Player").transform;
		enemy = GetComponent<NavMeshAgent>();
	}

	private void Update() {
		enemy.SetDestination(target.position);
	}
}
