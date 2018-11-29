// Author: Elisha Anagnostakis
// Date Modified: 16/11/18
// Purpose: This script allows chunk to destroy any object that has this sript on it 

using UnityEngine;

public class DestructableObjects : MonoBehaviour {

    // prop health
    public int maxHealth;
    public int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

   public void FixedUpdate()
    {
        // checks if the health is 0
        if (currentHealth <= 0)
        {
            // and destroys the object from the scene
            Destroy(obj: gameObject);
        }
    }
   
    // damage
   public void ObjectDamage(int _amount)
    {
        currentHealth -= _amount;
    }
}