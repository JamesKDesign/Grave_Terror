using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

    public float damage;
    public float idleDamage;

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().DamagePlayer(damage * idleDamage * Time.deltaTime);
        }
    }
}
