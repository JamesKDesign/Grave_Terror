using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWander : BaseBehaviour
{
	public EnemyWander(Enemy _enemy, float _weight = 1.0f) : base(_enemy, _weight)
	{

	}

	public override Vector3 Update()
	{
		Vector3 dev = enemy.velocity;
		float rval = Random.value;

		dev.x *= Mathf.Sin(rval);
		dev.z *= Mathf.Cos(rval);

		return Vector3.zero;
	}
}
