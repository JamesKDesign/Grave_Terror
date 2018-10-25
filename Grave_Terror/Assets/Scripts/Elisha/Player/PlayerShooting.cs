using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerShooting : MonoBehaviour
{

    public GameObject projectile;
    public GameObject hitEffect;
    public float delay;
    public float counter = 0f;
    public float damageToGive;
    public float impactForce = 30f;
    public XboxController controller;
    public float range;
    public PlayerMovement useController;

    // Raycasting
    private void FixedUpdate()
    {
        counter += Time.deltaTime;
        if (useController.useController == true)
        {
            if (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0.1f)
            {
                if (counter > delay)
                {
                    // bullets
                    GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
                    counter = 0f;

                    RaycastHit hit;
                    Ray rayCast = new Ray(transform.position, transform.forward);
                    if (Physics.Raycast(rayCast, out hit, range))
                    {
                        // enemy health damaged
                        EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                        if (target != null)
                        {
                            target.DamageHealth(damageToGive);
                            Destroy(bullet, 0.2f);
                        }

                        // force push back
                        if (hit.rigidbody != null)
                        {
                            hit.rigidbody.AddForce(-hit.normal * impactForce);
                        }

                        // blood
                        GameObject impactGo = Instantiate(hitEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        Destroy(impactGo, 0.2f);
                    }
                }
            }
        }
        else if (!useController.useController)
        {
            if (Input.GetKey(KeyCode.Mouse0) && counter > delay)
            {
                // bullets
                GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
                counter = 0f;

                RaycastHit hit;
                Ray rayCast = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(rayCast, out hit, range))
                {

                    // enemy health damaged
                    EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                    if (target != null)
                    {
                        target.DamageHealth(damageToGive);
                    }

                    // force push back
                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * impactForce);
                    }

                    // blood
                   GameObject impactGo = Instantiate(hitEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                   Destroy(impactGo.gameObject, 2.0f);
                }
            }
        }
    }
}