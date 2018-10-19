using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvade : BaseBehaviour
{
	//BulletManager?
	//___

	//Initialization
	public EnemyEvade(Enemy _enemy, float _weight = 1.0f) : base(_enemy, _weight)
	{

	}
	
	// Update is called once per frame
	override public Vector3 Update()
	{
		return Vector3.zero;
	}
}
