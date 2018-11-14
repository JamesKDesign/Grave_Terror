using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum E_STATE
{
	NORMAL,
	IGNITED
}

public enum E_ACTION
{
	IDLE,
	ATTACK,
	MOVE
}

[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
	//'enemy' HP values
	private static PlayerHealth p1HP, p2HP;

	[HideInInspector] //THe spawner that created us
	public Spawner spawner;

	//Our own HP
	private EnemyHealth health;

	[HideInInspector]
	public GameObject target = null;

	private CharacterController cc;

	[HideInInspector] //Current state
	public E_STATE state = E_STATE.NORMAL;

	//Is this actor alive?
	[HideInInspector]
	public bool alive = false;

	//Is this actor directly engaging the target?
	//[HideInInspector]

	//Values to be set in the editor
	[Tooltip("Actor attack damage")]
	public int attackDamage;
	[Tooltip("How long the actor takes to wind up his attack (in unity meters)")]
	public float attackDelay;
	[Tooltip("Time until next attack can be done after finishing an attack (delay in seconds)")]
	public float attackRate;
	[Tooltip("Actor travel speed")]
	public float movementSpeed;

	//What path this actors following
    [HideInInspector]
	public int path = -1;

	[HideInInspector]
	public bool engaging = false;

	//Deny the actor from moving and turning when he is attacking
	private bool attackLocked = false;
	private float attackTimer = 0.0f;
	private float attackRecov = 0.0f;
	private EnemyAttackBox attackHitbox = null;

	[HideInInspector]
	public float fireDamageOverTime = 0.0f;
	[HideInInspector]
	public float fireTime = 0.0f;

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
		behaviours.Add(new EnemyWander(this, wanderWeight)); //This one needs work

		cc = GetComponent<CharacterController>();

		health = GetComponent<EnemyHealth>();

		attackHitbox = transform.Find("AttackBox").GetComponent<EnemyAttackBox>();

		p1HP = FlowFieldGenerator.GetInstance().targets[0].GetComponent<PlayerHealth>();
		p2HP = FlowFieldGenerator.GetInstance().targets[1].GetComponent<PlayerHealth>();
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
				fireTime -= Time.deltaTime;
				//Enemy is no longer on fire
				if (fireTime <= 0.0f)
					state = E_STATE.NORMAL;
				break;
		}
		

		//If we have no path get one
		if (path == -1)
		{
			attackLocked = false;
			if (p1HP.playerState == PlayerHealth.PlayerState.ALIVE)
			{
				path = 0;
			}
			else if (p2HP.playerState == PlayerHealth.PlayerState.ALIVE)
			{
				path = 1;
			}
			else
			{
				return;
			}
		}

		//Debug.Log(path);
		//Just incase
		if (path == 0 && p1HP.playerState != PlayerHealth.PlayerState.ALIVE)
		{
			path = -1;
			engaging = false;
		}
		else if (path == 1 && p2HP.playerState != PlayerHealth.PlayerState.ALIVE)
		{
			path = -1;
			engaging = false;
		}

        //Cycle through all behaviours or attack
        if (engaging) //Attack
        {
            if (target == null)
            {
				engaging = false;
				return;
            }
			//Move directly towards our target
			acceleration = target.transform.position - transform.position;

			attackTimer -= Time.deltaTime;
			if (attackLocked)
			{
				if (attackTimer <= 0.0f && attackRecov <= 0.0f)
				{
					Attack();
					//Allow movement and rotation again
					attackLocked = false;
					//Set the time for cooldown between attacks
					attackRecov = attackRate;
				}
			}
		}
		else //Behaviours
		{
			//Deadlock prevention!
			attackLocked = false;

			acceleration = Vector3.zero;
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

		//Prevent movement while attacking ------
		if (attackLocked)
		{
			acceleration = Vector3.zero;
			velocity = Vector3.zero;
		}

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

		//Anchor the enemy to the floor (rigidbody raycast fix)
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
		if (Random.value > 0.5f)
			path = 0;
		else
			path = 1;
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
	public void Ignite(float _fire_strength = 10.0f, float _fire_duration = 5.0f)
	{
		state = E_STATE.IGNITED;
		fireDamageOverTime = _fire_strength;
		fireTime = _fire_duration;
	}

	//Don't call this anywhere but AttackBox
	//Signal to attempt an attack
	public void AttackSignal()
	{
		//Only attack if we can
		if (attackRecov <= 0.0f && attackTimer <= 0.0f)
		{
			attackLocked = true;
			attackTimer = attackDelay;
			//DEBUG
			//transform.Find("AttackWindup").GetComponent<ParticleSystem>().Play();
		}
	}
	//Attack and deal damage if a player was hit
	public void Attack()
	{
		//If there is no valid target in the attackbox then the attack is missed
		if (attackHitbox.target != null)
		{
			attackHitbox.target.transform.GetComponent<PlayerHealth>().DamagePlayer(attackDamage);
			if (attackHitbox.target.transform.GetComponent<PlayerHealth>().playerState == PlayerHealth.PlayerState.ALIVE)
			{
				//Set 0 to 1 and 1 to 0
				path = FlowFieldGenerator.GetInstance().AttemptTarget(path * -1 + 1);
			}
			//DEBUG
			//transform.Find("AttackEffect").GetComponent<ParticleSystem>().Play();
			Debug.Log("attacking " + attackHitbox.target.name);
		}
	}

	//private void OnDrawGizmos()
	//{
	//	Debug.DrawRay(transform.position, transform.forward * 10.0f, Color.red);
	//}
}
