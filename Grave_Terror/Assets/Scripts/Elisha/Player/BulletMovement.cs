using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    public float Speed;
    public float bulletLife;

    public int damage;

    // Update is called once per frame
    void Update()
    {

        // changing the objects transform by multiplying the vector3 forward by speed and deltatime
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        bulletLife -= Time.deltaTime;
        if (bulletLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}