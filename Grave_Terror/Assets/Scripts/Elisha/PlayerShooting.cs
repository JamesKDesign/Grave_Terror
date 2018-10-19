using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerShooting : MonoBehaviour {

    public GameObject projectile;
    public GameObject Blood;
    public float delay;
    public float counter = 0f;
    public float damage;
    public float impactForce = 30f;
    public XboxController controller;

    // Raycasting
    private void FixedUpdate()
    {
        counter += Time.deltaTime;
        if (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0.5f)
        {
            if(counter > delay)
            {
                // bullets
                GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
                Destroy(bullet, 3.0f);
                counter = 0f;

                RaycastHit hit;
                Ray rayCast = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(rayCast, out hit, 100f))
                {
                    // enemy health damaged
                    EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                    if (target != null)
                    {
                        target.DamageHealth(damage);
                    }

                    // force push back
                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * impactForce);
                    }

                    // blood
                    GameObject impactGo = Instantiate(Blood, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    Destroy(impactGo.gameObject, 2.0f);
                }
            } 
        }



        //if (Input.GetKey(KeyCode.Mouse0) && counter > delay)
        //{
        //    // bullets
        //    GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
        //    Destroy(bullet, 3.0f);
        //    counter = 0f;

        //    RaycastHit hit;
        //    Ray rayCast = new Ray(transform.position, transform.forward);
        //    if(Physics.Raycast(rayCast, out hit, 100f))
        //    {
        //        // enemy health damaged
        //        EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
        //        if (target != null)
        //        {
        //            target.DamageHealth(damage);
        //        }

        //        // force push back
        //        if(hit.rigidbody != null)
        //        {
        //            hit.rigidbody.AddForce(-hit.normal * impactForce);
        //        }

        //        // blood
        //        GameObject impactGo = Instantiate(Blood, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
        //        Destroy(impactGo.gameObject, 2.0f);
        //    }
        //}
    }
}