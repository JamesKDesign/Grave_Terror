using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum E_STATE
{
	IDLE,
	PATHING,
	ENGAGING
}
public class Enemy : MonoBehaviour
{
	[HideInInspector] //THe spawner that created us
	public Spawner spawner;

	public GameObject target;

	private Rigidbody rb;

	[HideInInspector] //Current state
	public E_STATE state = E_STATE.IDLE;
	[HideInInspector] //Copy to the path in behaviour pathing (if valid)
	public NavMeshPath path = null;

	//Is this actor alive?
	[HideInInspector]
	public bool alive = false;

	//Values to be set in the editor
	[Tooltip("Actor attack damage")]
	public int attackDamage;
	[Tooltip("Actor attack range (in unity meters)")]
	public float attackRange;
	[Tooltip("Actor travel speed")]
	public float movementSpeed;

	//Public so the controller can easily access it
	[HideInInspector]
	public List<BaseBehaviour> behaviours = new List<BaseBehaviour>(4);

	private Vector3 suggestedDirection;

	// Use this for initialization
	void Start ()
	{
		behaviours.Add(new EnemyPathing(this));
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Cycle through all behaviours
		suggestedDirection = Vector3.zero;
		foreach (BaseBehaviour b in behaviours)
		{
			suggestedDirection += b.Update();
		}
		//No up velocity
		suggestedDirection.y = 0.0f;
		//Normalize it and multiply it by our speed
		suggestedDirection.Normalize();
		suggestedDirection *= (movementSpeed * Time.fixedDeltaTime);

		//rotate towards where we are going
		if(suggestedDirection != Vector3.zero)
			transform.rotation.SetLookRotation(suggestedDirection);
	}

	private void FixedUpdate()
	{
		transform.Translate(suggestedDirection);
	}

	//Newly spawned or just respawned
	public void Init()
	{
		alive = true;
		//Tell the EnemyController we are alive
		//EnemyController.instance.RegisterEnemy(this);
	}
	//Actor died
	void Dead()
	{
		//Prevents this from being called twice
		if (alive)
		{
			alive = false;

			//EnemyController.instance.DeregisterEnemy(this);
			spawner.Despawn(gameObject);
		}
	}

	public void ResetBehaviours()
	{
		foreach (BaseBehaviour b in behaviours)
		{
			b.Reset();
		}
	}
}
