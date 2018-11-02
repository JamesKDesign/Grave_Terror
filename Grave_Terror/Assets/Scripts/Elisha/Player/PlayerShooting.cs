﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerShooting : MonoBehaviour
{

    public GameObject particleProjectile;
    public GameObject hitEffect;
    public ParticleSystem muzzleFlash;
    public float delay;
    public float counter = 0f;
    public int damageToGive;
    public float impactForce = 30f;
    public float range;
    public CameraShake cameraShake;
    public float duration;
    public XboxControllerManager xboxController;

    // Raycasting
    private void FixedUpdate()
    {
        counter += Time.deltaTime;
        if (xboxController.useController == true)
        {
            if (XCI.GetAxis(XboxAxis.RightTrigger, xboxController.controller) > 0.1f)
            {
                if (counter > delay)
                {
                    muzzleFlash.Play();
                     // bullets
                    GameObject bullet = Instantiate(particleProjectile, transform.position, transform.rotation);
                    
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
                            //cameraShake.Shake(duration);
                        }

                        // force push back
                        if (hit.rigidbody != null)
                        {
                            hit.rigidbody.AddForce(-hit.normal * impactForce);
                            cameraShake.Shake(duration);
                        }

                        DestructableObjects obj = hit.transform.GetComponent<DestructableObjects>();
                        if (obj != null)
                        {
                            obj.ObjectDamage(damageToGive);
                        }

                        // blood
                        GameObject impactGo = Instantiate(hitEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        Destroy(impactGo, 0.2f);
                    }
                }
            }
            else
            {
                muzzleFlash.Stop();
            }
        }
        else if (!xboxController.useController)
        {
            if (Input.GetKey(KeyCode.Mouse0) && counter > delay)
            {
                // bullets
                GameObject bullet = Instantiate(particleProjectile, transform.position, transform.rotation);
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

                    DestructableObjects obj = hit.transform.GetComponent<DestructableObjects>();
                    if(obj != null)
                    {
                        obj.ObjectDamage(damageToGive);
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

    //public void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Finish")
    //    {
    //        other.gameObject.GetComponent<DestructableObjects>().ObjectDamage(damageToGive);
    //    }
    //}
}