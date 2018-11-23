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
    public ParticleSystem bulletCasing;
    public XboxControllerManager xboxController;
    private GameObject bullet;
    public float delay;
    public float counter = 0f;
    public int damageToGive;
    public float range;
    public EnemyHealth target;
    public PlayerHealth health;
    Vector3 hitLocation = Vector3.zero;
    public new Animator anim;
    public CameraController cameraController;
    public LayerMask layerMask;

    void Start()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    // Raycasting
    private void FixedUpdate()
    {
        Shooting();
        Aim();
    }

    void Shooting()
    {
        if (health.playerState == PlayerHealth.PlayerState.ALIVE)
        {
            if (xboxController.useController == true)
            {
                counter += Time.deltaTime;
                if (health.currentHealth > 0)
                {
                    if (XCI.GetAxis(XboxAxis.RightTrigger, xboxController.controller) > 0.1f)
                    {
                        if (counter > delay)
                        {
                            //anim.SetBool("isShooting", true);
                            muzzleFlash.Play();
                            bulletCasing.Play();

                            // bullets
                            //anim.SetBool("IsAttacking", true);
                            bullet = Instantiate(particleProjectile, transform.position, chunkRotation.transform.rotation);
                            counter = 0f;

                            anim.SetBool("IsAttacking", true);


                            RaycastHit hit;
                            Ray rayCast = new Ray(transform.position, transform.forward);
                            if (Physics.Raycast(rayCast, out hit, range, layerMask))
                            {
                                // enemy health damaged
                                target = hit.transform.GetComponent<EnemyHealth>();
                                if (target != null)
                                {
                                    target.DamageHealth(damageToGive);
                                    // blood

                                    cameraController.ShakeCamera();
                                    Debug.Log("shake");

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
                                    Debug.DrawLine(transform.position, hit.point, Color.red);
                                }

                            }
                        }
                    }
                    else if (XCI.GetAxis(XboxAxis.RightTrigger, xboxController.controller) < 0.1)
                    {
                        muzzleFlash.Stop();
                        bulletCasing.Stop();

                        anim.SetBool("IsAttacking", false);
                    }
                }
            }
            else if (health.playerState == PlayerHealth.PlayerState.REVIVE && health.playerState == PlayerHealth.PlayerState.DEAD)
            {
                xboxController.useController = false;
            }
        }
    }

    void Aim()
    {
        //Raycasting from the player's position and creating an array of hit results from players forward direction
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position, transform.forward, 100.0f);
        Debug.DrawRay(transform.position, transform.forward, Color.magenta);
        foreach (RaycastHit result in hit)
        {
            //If the cast hits an object tagged as 'enemy', run settargeted function on Enemy script
            if (result.collider.gameObject.tag == "Enemy")
            {
                Debug.Log("Enemy Targeted");
                hitLocation = result.point;
                result.collider.gameObject.GetComponent<Enemy>().SetTargeted();

                break;
            }
        }
    }

    //Debug cube on targeted location
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(hitLocation, Vector3.one);
    }
}