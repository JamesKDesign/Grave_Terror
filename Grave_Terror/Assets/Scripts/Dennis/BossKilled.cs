using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BossKilled : MonoBehaviour
{
    public UnityEvent triggers;

    private void Update()
    {
        if (GetComponent<EnemyHealth>().currentHealth <= 0.0f)
        {
            triggers.Invoke();
            Destroy(this);
        }
    }
}
