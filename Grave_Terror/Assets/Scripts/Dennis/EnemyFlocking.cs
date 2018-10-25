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
		Vector3 averageVel = Vector3.zero;
		Vector3 averageNUL = Vector3.zero;
		int i = 0;
		for(; i < sensor.nearby.Count; ++i)
		{
			averageVel += sensor.nearby[i].velocity;
			averageNUL += sensor.nearby[i].transform.position;
			//Limiter of 4 to early out
			if (i > 4)
				break;
		}
		//Velocity
		averageVel /= (float)i;
		//Unnamed seperator
		averageNUL /= (float)i;
		averageNUL = averageNUL - enemy.transform.position;

		return Vector3.Normalize(averageVel + averageNUL);
	}
}
