using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ResistanceTable //Anything taking damage should use this
{
	public float fireSoak;
	public float physicalSoak;
}

[System.Serializable] //Anything dealing damage should use this
public struct Damage
{
	public float fireDamage;
	public float physicalDamage;
}

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] private float maxHealth;
	[SerializeField] private float health;
	private bool isDead;
	// enemy CapCollider
	CapsuleCollider collider;

	//Regen rate
	public float healthRegen;
	[Tooltip("Don't regen health if hit in the last 'x' seconds")]
	public float healthRegenDelay;

	private float timer = 0.0f;

	public ResistanceTable resistances;

	void Awake() {
		// collider = GetComponent <CapsuleCollider> ();
		collider = GetComponent<CapsuleCollider>();
	}

	private void Update()
	{
		timer += Time.deltaTime;

		if (timer > healthRegenDelay)
		{
			Heal(healthRegen * Time.deltaTime);
		}
	}

	//Take damage
	void TakeDamage(Damage _damage)
	{
		//Subtract soak from damage then...
		float totalDamage = 0;
		totalDamage += _damage.fireDamage * resistances.fireSoak;
		totalDamage += _damage.physicalDamage * resistances.physicalSoak;
		//...apply the damage
		health -= totalDamage;

		//Clamp health to 0
		if (health <= 0)
			health = 0;
			Death();

		//Just took damage, reset timer
		timer = 0.0f;
	}

	void Heal(float _heal_amount)
	{
		health += _heal_amount;

		//Prevent health going over max
		if (health >= maxHealth)
			health = maxHealth;
	}

	void Death() {
		isDead = true;
		collider.isTrigger = true;
		Destroy(gameObject, 5.0f);
	}
}