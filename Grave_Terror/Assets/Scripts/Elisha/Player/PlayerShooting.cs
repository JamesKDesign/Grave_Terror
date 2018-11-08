using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerShooting : MonoBehaviour
{

    public GameObject particleProjectile;
    public GameObject hitEffect;
    public GameObject chunkRotation;
    public ParticleSystem muzzleFlash;
    public XboxControllerManager xboxController;
    //public Animator anim;
    private GameObject bullet;
    public float delay;
    public float counter = 0f;
    public int damageToGive;
    public float range;
    public float duration;
    public int maxAmmo;
    public int currentAmmo;
    private bool isReloading = false;


    private void Awake()
    {
        currentAmmo = maxAmmo;
        //anim = GetComponent<Animator>();
    }

    // Raycasting
    private void FixedUpdate()
    {
        if (xboxController.useController == true)
        {
            
            counter += Time.deltaTime;
            if (XCI.GetAxis(XboxAxis.RightTrigger, xboxController.controller) > 0.1f)
            {
                if(isReloading == false)
                {
                    currentAmmo--;
                    if (counter > delay)
                    {
                        //anim.SetBool("isShooting", true);
                        muzzleFlash.Play();
                        // bullets
                        bullet = Instantiate(particleProjectile, transform.position, chunkRotation.transform.rotation);
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
                else if(isReloading == true)
                {
                    //anim.SetBool("isShooting", false);
                    if (XCI.GetButtonDown(XboxButton.X))
                    {
                        currentAmmo = maxAmmo;
                        isReloading = false;
                    }
                }
            }
            else if (XCI.GetAxis(XboxAxis.RightTrigger, xboxController.controller) < 0.1)
            {
               // anim.SetBool("isShooting", false);
                muzzleFlash.Stop();
            }
        }
        else if (!xboxController.useController)
        {
            if (Input.GetKey(KeyCode.Mouse0) && counter > delay)
            {
                // bullets
                bullet = Instantiate(particleProjectile, transform.position, transform.rotation);
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

                    // blood
                   GameObject impactGo = Instantiate(hitEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                   Destroy(impactGo.gameObject, 2.0f);
                }
            }
        }

        if (currentAmmo <= 0)
        {
            isReloading = true;
            currentAmmo = 0;
            bullet.SetActive(false);
            muzzleFlash.Stop();
        }
    }
}