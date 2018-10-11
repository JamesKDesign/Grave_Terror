using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour
{
	//Weight of this behaviour
	public float weight;
	//The enemy we are attached too
	public Enemy enemy;

	//Initialization
	public BaseBehaviour(Enemy _enemy)
	{
		enemy = _enemy;
	}

	//Called by the attached enemy
	virtual public Vector3 Update()
	{
		return Vector3.zero;
	}

	//For use by the enemyController
	virtual public void Reset()
	{

	}
}
