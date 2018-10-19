using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlocking : BaseBehaviour
{
	EnemyProximitySensor sensor = null;

	public EnemyFlocking(Enemy _enemy, float _weight = 1.0f) : base(_enemy, _weight)
	{
		sensor = _enemy.GetComponentInChildren<EnemyProximitySensor>(true);
	}

	override public Vector3 Update()
	{
		Vector3 averagePos = Vector3.zero;
		Vector3 averageVel = Vector3.zero;
		Vector3 averageNUL = Vector3.zero;
		int i = 0;
		for(; i < sensor.nearby.Count; ++i)
		{
			//averagePos += sensor.nearby[i].transform.position;
			averageVel += sensor.nearby[i].velocity;
			averageNUL += sensor.nearby[i].transform.position;
		}
		//Position
		averagePos /= (float)i;
		averagePos = averagePos - enemy.transform.position;
		//Velocity
		averageVel /= (float)i;
		//Unnamed seperator
		averageNUL /= (float)i;
		averageNUL = averageNUL - enemy.transform.position;

		return Vector3.Normalize(averageVel + averageNUL);
	}
}
