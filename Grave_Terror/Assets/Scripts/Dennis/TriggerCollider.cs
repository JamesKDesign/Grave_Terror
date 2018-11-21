using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCollider : MonoBehaviour
{

	public UnityEvent triggers;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            triggers.Invoke();
    }

    private void OnDrawGizmos()
    {
        Bounds bound = GetComponent<BoxCollider>().bounds;
        Gizmos.DrawWireCube(bound.center, bound.extents * 2.0f);
    }
}
