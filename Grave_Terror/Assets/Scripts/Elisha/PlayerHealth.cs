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

        while(isAlive)
        {
            if (health <= 0)
            {

                Death();
            //    deathTimer -= Time.deltaTime;

            //    if (deathTimer <= 0)
            //    {
            //        Death();
            //    }
            //    else
            //    {
            //        downedState();
            //        if (Vector3.Distance(transform.position, sizzle.position) <= reviveRadius)
            //        {
            //            reviveTimer -= Time.deltaTime;
            //            if (reviveTimer <= 0)
            //            {
            //                health = maxHealth;
            //                break;
            //            }
            //            break;
            //        }
            //        break;
            //    } 
            //}
            //else
            //{
            //    continue;
            }
            break;
        }
    }

    // when down player will hardly be able to move
    private void downedState()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(0, 0, 1 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(-1 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0, 0, -1 * Time.deltaTime);
        }
    }

    // player death
    public void Death()
    {
        isAlive = false;
        Destroy(this.gameObject);
    }
}