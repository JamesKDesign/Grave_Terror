using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDestroy : MonoBehaviour
{
	public UnityEvent triggers;
	private void OnDestroy()
	{
		triggers.Invoke();
	}
}
