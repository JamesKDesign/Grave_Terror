// Author: Elisha Anagnostakis
// Date Modified: 20/11/18
// Purpose: This script creates the forward movement of the bullet trail on chunks machine gun

using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [Tooltip("Speed at which the bullet travels")]
    public float Speed;
    [Tooltip("How long the bullet lasts in the scene when shot")]
    public float bulletLife;

    // Update is called once per frame
    void FixedUpdate()
    {
        // changing the objects transform by multiplying the vector3 forward by speed and deltatime
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        bulletLife -= Time.deltaTime;
        // destroys trail 
        if (bulletLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // if bullet collides with any object
        if (other.gameObject)
        {
            // delete it
            Destroy(gameObject);
        }
    }
}