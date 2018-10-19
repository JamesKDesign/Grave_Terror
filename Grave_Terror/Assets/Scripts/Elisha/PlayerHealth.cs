using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    // player health
    public float health;
    // radius at which you can revive the player
    public float reviveRadius;
    // sizzle player transform
    public Transform sizzle;
    // max health
    public int maxHealth;
    // checks if player is alive
    bool isAlive = true;
    // time it takes to revive player
    public float reviveTimer;
    // time it takes to die when down
    public float deathTimer;
    
    // player damage loss
    public void RemoveHealth(float amount)
    {
        health -= amount;

        if (health <= 0)
        {

          Death();
           
        }
  
    }

    // when down player will hardly be able to move
    private void downedState()
    {
      
    }

    // player death
    public void Death()
    {
        isAlive = false;
        Destroy(gameObject);
    }
}