using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [Tooltip("Damage amount the player will take when hit")]
    public float damage;
    [Tooltip("Damage amount the player will take when the player isnt moving")]
    public float idleDamage;

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().DamagePlayer(damage * idleDamage * Time.deltaTime);
        }
    }
}