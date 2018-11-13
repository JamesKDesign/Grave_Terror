using UnityEngine;
using XboxCtrlrInput;

public class PlayerShooting : MonoBehaviour
{
    public GameObject particleProjectile;
    public GameObject bloodEffect;
    public GameObject bulletHole;
    private GameObject holes;
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
                            // blood
                            GameObject impact = Instantiate(bloodEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                            Destroy(impact, 0.5f);
                        }

                        DestructableObjects obj = hit.transform.GetComponent<DestructableObjects>();
                        if (obj != null)
                        {
                            obj.ObjectDamage(damageToGive);
                            // blood
                            holes = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                            Destroy(holes, 0.2f);
                        }
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
            counter += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Mouse0) && counter > delay)
            {
                if (counter > delay)
                {
                    //anim.SetBool("isShooting", true);
                    muzzleFlash.Play();
                    // bullets
                    bullet = Instantiate(particleProjectile, transform.position, chunkRotation.transform.rotation);
                    Destroy(bullet, 1f);
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
                            // blood
                            GameObject impact = Instantiate(bloodEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                            Destroy(impact, 0.5f);
                        }

                        DestructableObjects obj = hit.transform.GetComponent<DestructableObjects>();
                        if (obj != null)
                        {
                            obj.ObjectDamage(damageToGive);
                            // blood
                            holes = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                            Destroy(holes, 0.2f);
                        }
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0) && counter < delay)
            {
                muzzleFlash.Stop();
                anim.SetBool("IsAttacking", false);
            }
        }
    }
}