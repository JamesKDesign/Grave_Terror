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
		FlowFieldGenerator.Segment seg = FFG.GetSegment(enemy.transform.position);
		if (seg != null)
			lastHeading = seg.direction;
		return lastHeading;
	}

	//Back to 1
	override public void Reset()
	{
		
	}
}
