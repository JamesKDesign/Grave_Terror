using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathing : BaseBehaviour
{
	//easy access to the flow field generator for all pathing behaviours
	private static FlowFieldGenerator FFG;

	private Vector3 lastHeading = Vector3.zero;

	//Initialization
	public EnemyPathing(Enemy _enemy, float _weight = 1.0f) : base(_enemy, _weight)
	{
		FFG = FlowFieldGenerator.GetInstance();
	}
	
	// Update is called once per frame
	override public Vector3 Update()
	{
		if (enemy.path == -1)
			return Vector3.zero;
		//If we're on the grid just follow its directions
		Vector3 seg = FFG.GetSegmentDirection(enemy.transform.position, enemy.path);
		if (seg != null)
		{
			lastHeading = seg;
			return lastHeading;
		}
		//If we're off the grid
		else
		{
			return -lastHeading * 1000.0f;
		}
	}

	//Back to 1
	override public void Reset()
	{
		
	}
}
