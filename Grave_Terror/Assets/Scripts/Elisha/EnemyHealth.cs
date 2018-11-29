// Author: Elisha Anagnostakis
// Date Modified: 28/11/18
// Purpose: This script managers the enemies health 

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public float currentHealth;

    private void Awake()
    {
        currentHealth = health;
    }

    private void Update()
    {
        // if enemy health is 0
        if (currentHealth <= 0)
        {
            // call dead function from enemy script
            gameObject.GetComponent<Enemy>().Dead();
        }
    }
    // damage the enemy takes constantly
    public void DamageHealth(float amount)
    {
        currentHealth -= amount;
    }
}