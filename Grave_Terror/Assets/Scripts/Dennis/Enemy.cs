using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum E_STATE
{
	NORMAL,
	IGNITED
}

[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
	[HideInInspector] //THe spawner that created us
	public Spawner spawner;

	private EnemyHealth health;

	[HideInInspector]
	public GameObject target;

	private CharacterController cc;

	[HideInInspector] //Current state
	public E_STATE state = E_STATE.NORMAL;

	//Is this actor alive?
	[HideInInspector]
	public bool alive = false;

	//Is this actor directly engaging the target?
	[HideInInspector]
	public bool engaging = false;
	private BaseBehaviour engager;

	//Values to be set in the editor
	[Tooltip("Actor attack damage")]
	public int attackDamage;
	[Tooltip("Actor attack range (in unity meters)")]
	public float attackRange;
	[Tooltip("Actor attack rate (delay in seconds)")]
	public float attackRate;
	[Tooltip("Actor travel speed")]
	public float movementSpeed;

	[HideInInspector]
	public float fireDamageOverTime = 0.0f;

	//Public so the controller can easily access it
	[HideInInspector]
	public List<BaseBehaviour> behaviours = new List<BaseBehaviour>(4);

	[HideInInspector]
	public Vector3 velocity;
	private Vector3 acceleration;

	//Slowdown overtime
	public float drag = 0.97f;
	[Tooltip("Higher mass means it takes longer to reach max speed")]
	public float mass = 10.0f;

	[Header("Weights")]
	public float pathingWeight = 0.67f;
	public float flockingWeight = 0.33f;
	public float wanderWeight = 0.10f;

	// Use this for initialization
	void Start ()
	{
		behaviours.Add(new EnemyPathing(this, pathingWeight));
		behaviours.Add(new EnemyFlocking(this, flockingWeight));
		behaviours.Add(new EnemyWander(this, wanderWeight));
		//Moved to its own section
		engager = new EnemyAttack(this, 1.0f); //Weight does not effect this behaviour

		cc = GetComponent<CharacterController>();

		health = GetComponent<EnemyHealth>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (state)
		{
			case E_STATE.NORMAL:
				//Disable fire effect here?
				break;

			case E_STATE.IGNITED:
				//Mabye display some fire effect here?
				health.DamageHealth(fireDamageOverTime * Time.deltaTime);
				break;
		}

		//Cycle through all behaviours
		acceleration = Vector3.zero;
		if (engaging)
		{
			acceleration += engager.Update();
		}
		else
		{
			foreach (BaseBehaviour b in behaviours)
			{
				acceleration += b.Update() * b.weight;
			}
		}
		//No up velocity
		acceleration.y = 0.0f;
		//Normalize it and multiply it by our speed
		acceleration.Normalize();
		acceleration *= movementSpeed;

		velocity += acceleration / mass;
		velocity *= drag;

		//rotate towards where we are going
		if (velocity != Vector3.zero)
			transform.localRotation = Quaternion.LookRotation(velocity);
			//transform.rotation.SetFromToRotation(Vector3.zero, velocity);

		//Cap max velocity
		if (velocity.magnitude > movementSpeed)
		{
			velocity = Vector3.Normalize(velocity) * movementSpeed;
		}

		//Anchor the enemy to the floor (rigidbody fix)
		transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
	}

	private void FixedUpdate()
	{
		//Debug.Log("WTF " + velocity);
		cc.Move(velocity * Time.deltaTime);
	}

	//Newly spawned or just respawned
	public void Init()
	{
		alive = true;
		//Tell the EnemyController we are alive
		//EnemyController.instance.RegisterEnemy(this);
	}
	//Actor died
	public void Dead()
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

	//Ignite this enemy
	public void Ignite(float _fire_strength = 10.0f)
	{
		state = E_STATE.IGNITED;
		fireDamageOverTime = _fire_strength;
	}

	private void OnDrawGizmos()
	{
		Debug.DrawRay(transform.position, transform.forward * 10.0f, Color.red);
	}
}
