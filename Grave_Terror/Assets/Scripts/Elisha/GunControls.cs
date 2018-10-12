using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunControls : MonoBehaviour {

	// checks if player is or isnt firing
	public bool isFiring = false;
	// referencing bulletMovement script
	[SerializeField] private BulletMovement bullet;
	// Bullets speed
	[SerializeField] private float bulletSpeed;
	// How many bullets fire at a time
	[SerializeField] private float shotCounter;
	// Time between bullets spawning 
	[SerializeField] private float timer;
	// Transform for invisible spawn point for the bullet
	[SerializeField] private Transform firePoint;
	private float damagePerShot;
	private float bulletRange;
	// reference to enemy health script
	EnemyHealth enemyHealth;
    
  

	// Update is called once per frame
	void Update () {
        FireGun();
	}

    private void FireGun()
    {
       
            // checks if the player is firing
            if (isFiring)
            {
                // counter starts
                shotCounter -= Time.deltaTime;
                // if the counter is less than or equal to 0 THEN
                if (shotCounter <= 0)
                {
                    // set the time between shots to equal the counter
                    shotCounter = timer;
                    // creates a new bullet using the Instantiate function which copies the object and its properties within the parameters
                    BulletMovement newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity) as BulletMovement;
                    // Have the speed from the bullet equal the bullet speed from the gun 
                    newBullet.Speed = bulletSpeed;
                    Destroy(newBullet.gameObject, 1.0f);
                }
            }
            else // If the above statement is false
            {
                // have the counter equal 0
                shotCounter = 0;
            }
        }
    }