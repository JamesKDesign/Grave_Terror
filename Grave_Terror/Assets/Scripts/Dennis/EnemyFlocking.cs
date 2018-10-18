using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlocking : BaseBehaviour
{
	EnemyProximitySensor sensor = null;

	public EnemyFlocking(Enemy _enemy) : base(_enemy)
	{
		sensor = _enemy.GetComponent<EnemyProximitySensor>();
	}

	override public Vector3 Update()
	{
		return Vector3.zero;
	}
}
