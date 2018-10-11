using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPursue : BaseBehaviour
{
	//Initialization
	public EnemyPursue(Enemy _enemy) : base(_enemy)
	{
	}

	override public Vector3 Update()
	{
		//Break conditions
		if (enemy.target == null)
			return Vector3.zero;

		//Time taken to reach target (ignoring all obstructions)
		float timeTaken = 1.0f;//Set to 1 for now because we have no velocity (Vector3.Magnitude(enemy.target.transform.position - enemy.transform.position) / Vector3.Magnitude(enemy.GetComponent<Rigidbody>().velocity));
		//Attempt to predict where the target will be based on its velocity
		//The futher away we are the more ahead we should predict
		Vector3 predicted = enemy.target.transform.position - enemy.transform.position;

		return predicted;
	}
}
