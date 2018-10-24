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
		//Needs testing
		Vector3 dev = enemy.transform.forward;
		//haha unity fu
		//dev *= Vector3.zero;

		return dev;
	}
}
