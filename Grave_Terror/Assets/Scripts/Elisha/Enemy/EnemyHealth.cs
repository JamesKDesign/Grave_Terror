using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int health;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = health;
        print("Enemy health is " + currentHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            ScoreBoard.score += 1;
            Destroy(gameObject);
            //gameObject.GetComponent<Enemy>().Dead();
            print("enemy dead");
        }
    }

    public void DamageHealth(float amount)
    {
        currentHealth -= amount;
        print("Enemy health is " + currentHealth);
    }
}