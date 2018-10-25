using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//partial class Vector3
//{
//	public static Vector3 operator *(Vector3 _rhs, Vector3 _lhs)
//	{
//		return new Vector3(_rhs.x * _lhs.x, _rhs.y * _lhs.y, _rhs.z * _lhs.z);
//	}
//}

public class EnemyAttack : BaseBehaviour
{
	private float timer = 0.0f;
	//Initialization
	public EnemyAttack(Enemy _enemy, float _weight = 0.0f) : base(_enemy, _weight)
	{
	}

	// Update is called once per frame
	override public Vector3 Update ()
	{
		if (enemy.target == null)
			return Vector3.zero;
		//If the enemy is close enough attack him
		if (enemy.attackRange * enemy.attackRange < Vector3.SqrMagnitude(enemy.transform.position - enemy.target.transform.position))
		{
			//Cooldown on attacking so we dont hit every frame
			timer += Time.deltaTime;
			if (timer <= 0.0f)
			{
				timer = enemy.attackRate;
				enemy.target.GetComponent<PlayerHealth>().DamagePlayer(enemy.attackDamage);
			}
		}
		//Non-directing behaviour
		return enemy.target.transform.position - enemy.transform.position;
	}
}
