// Author: Elisha Anagnostakis
// Date Modified: 20/11/18
// Purpose: This script allows for zmobies to be set on fire when sizzle shoots them down with her flame thrower

using UnityEngine;

public class ObjectOnFire : MonoBehaviour {

    public GameObject onFireEffect;
    private float timer;
    public float burnTime = 4.0f; 

	// Use this for initialization
	void Awake () {
        timer = 0.0f;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        // checks if the timer is greater than the burn time THEN 
        if(timer > burnTime)
        {
            // stop the fire particle
            timer = 0.0f;
            onFireEffect.GetComponentInChildren<ParticleSystem>().Stop();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // if the flame collider hits an enemy
        if(other.gameObject.tag == "Fire")
        {
            // set the fire particle on the enemy
            timer = 0.0f;
            onFireEffect.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}