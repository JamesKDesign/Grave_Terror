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
		//Multiplying the dev Vec3 with a Vec3(10,0,10)
		dev = new Vector3(dev.x * 1.0f, 0.0f, dev.z * 1.0f);

		float rValue = Random.Range(-1.0f, 1.0f);
		dev = new Vector3(dev.x + (enemy.transform.right.x * rValue), 0.0f, dev.z + (enemy.transform.right.z * rValue));

		return dev;
	}
}
