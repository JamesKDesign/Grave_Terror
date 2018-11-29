using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDestroyOther : MonoBehaviour
{
	public GameObject[] waitForDestruction;

	public UnityEvent triggers;
	private void Update()
	{
		foreach (GameObject o in waitForDestruction)
		{
			if (o == null)
			{
				continue;
			}
			return;
		}
		triggers.Invoke();
		//jobs done
		Destroy(this);
	}
}
