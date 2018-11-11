using UnityEngine;
using XboxCtrlrInput;

public class PlayerShooting : MonoBehaviour
{
    public GameObject particleProjectile;
    public GameObject hitEffect;
    public GameObject chunkRotation;
    public ParticleSystem muzzleFlash;
    public XboxControllerManager xboxController;
    private GameObject bullet;
    public float delay;
    public float counter = 0f;
    public int damageToGive;
    public float range;

    public new Animator anim;

    // Raycasting
    private void FixedUpdate()
    {
        if (xboxController.useController == true)
        {
            
            counter += Time.deltaTime;
            if (XCI.GetAxis(XboxAxis.RightTrigger, xboxController.controller) > 0.1f)
            {
                 
               if (counter > delay)
               {
                   //anim.SetBool("isShooting", true);
                   muzzleFlash.Play();
                   // bullets
                   bullet = Instantiate(particleProjectile, transform.position, chunkRotation.transform.rotation);
                   counter = 0f;

                    anim.SetBool("IsAttacking", true);

                    RaycastHit hit;
                   Ray rayCast = new Ray(transform.position, transform.forward);
                   if (Physics.Raycast(rayCast, out hit, range))
                   {
                       // enemy health damaged
                       EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                       if (target != null)
                       {
                           target.DamageHealth(damageToGive);
                            Destroy(bullet, 0.5f);
                       }

                       DestructableObjects obj = hit.transform.GetComponent<DestructableObjects>();
                       if (obj != null)
                       {
                           obj.ObjectDamage(damageToGive);
                           Destroy(bullet, 0.5f);
                       }

                       // blood
                       GameObject impactGo = Instantiate(hitEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                       Destroy(impactGo, 0.2f);
                   }
               }
            }
            else if (XCI.GetAxis(XboxAxis.RightTrigger, xboxController.controller) < 0.1)
            {
                muzzleFlash.Stop();

                anim.SetBool("IsAttacking", false);
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
    }
}