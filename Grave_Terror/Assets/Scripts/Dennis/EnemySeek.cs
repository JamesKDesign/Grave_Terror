using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeek : BaseBehaviour
{
	//What to pursue
	public Vector3 targetPos;

	//Initialization
	public EnemySeek(Enemy _enemy) : base(_enemy)
	{
		targetPos = Vector3.zero;
	}

	override public Vector3 Update()
	{
		//Move directly towards the target position, no questions asked
		return (targetPos - enemy.transform.position) * weight;
	}
}
