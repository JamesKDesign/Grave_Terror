using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This behaviour should only be assigned to a horde leader
public class EnemyPathing : BaseBehaviour
{
	//Path index (starts at 1 because navmeshpath's first point is current)
	private int pathIndex = 1;

	//How close to get to the path point
	public static float thresholdDistance = 0.25f;

	//Initialization
	public EnemyPathing(Enemy _enemy) : base(_enemy)
	{

	}
	
	// Update is called once per frame
	override public Vector3 Update()
	{
		//No path so dont do anything
		if (enemy.path == null)
		{
			return Vector3.zero;
		}
		//Dont do anything if we are already at the end of the path
		if (pathIndex >= enemy.path.corners.Length)
		{
			return Vector3.zero;
		}
		//If we get close enough to the pathing node, go to the next one
		if (Vector3.SqrMagnitude(enemy.path.corners[pathIndex] - enemy.transform.position) <= thresholdDistance * thresholdDistance)
		{
			pathIndex++;
			if (pathIndex == enemy.path.corners.Length)
				return Vector3.zero;
		}
		//Return where to go
		return (enemy.path.corners[pathIndex] - enemy.transform.position);
	}

	//Back to 1
	override public void Reset()
	{
		pathIndex = 1;
	}
}
