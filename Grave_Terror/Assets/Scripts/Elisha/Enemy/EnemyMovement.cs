using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	// TESTING PURPOSES ONLY //

    public Transform target;
    NavMeshAgent enemy;
	
	private void Awake() {
        target = GetComponent<Transform>();
		target = GameObject.FindGameObjectWithTag("Player").transform;

        enemy = GetComponent<NavMeshAgent>();
	}

	private void Update() {
		enemy.SetDestination(target.position);
    }
}