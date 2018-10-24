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
		dev = Vector3.Cross(dev, enemy.transform.right);

		dev.x += Random.Range(-1.0f, 1.0f);

		return dev;
	}
}
