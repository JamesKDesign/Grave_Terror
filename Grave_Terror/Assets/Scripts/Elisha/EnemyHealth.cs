using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float currentHealth;
   

    public void DamageHealth(float amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            ScoreBoard.score += 1;
            Destroy(gameObject);
        }
    }
}