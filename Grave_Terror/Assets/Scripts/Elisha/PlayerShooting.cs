// Author: Elisha Anagnostakis
// Date Modified: 29/11/18
// Purpose: This script handles chunks shooting by using ray casts and handles all the animations for the gun and the sounds

using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerShooting : MonoBehaviour
{
    // Script references
    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletCasing;
    public XboxControllerManager xboxController;
    public CameraController cameraController;
    public EnemyHealth target;
    public PlayerHealth health;
    public LayerMask layerMask;

    // Game objects that hold the particle effects
    public GameObject particleProjectile;
    public GameObject bloodEffect;
    public GameObject bulletHole;
    private GameObject holes;
    public GameObject chunkRotation;

    [Tooltip("The delay at which the gun then fires again")]
    public float delay;
    [Tooltip("constant counter on when you will fire")]
    public float counter = 0f;
    [Tooltip("damage conflicted on the enemy")]
    public int damageToGive;
    [Tooltip("Rnage at which the ray cast can shoot")]
    public float range;
    [Tooltip("Finds the location thats been hit")]
    Vector3 hitLocation = Vector3.zero;
    [Tooltip("Animation reference")]
    public new Animator anim;

    // audio clips
    public List<AudioClip> gunShots = new List<AudioClip>();
    public List<AudioClip> shellCasings = new List<AudioClip>();
    public List<AudioClip> bulletImpact = new List<AudioClip>();
    private AudioSource audioSource;
    public float minPitch = 0.0f;
    public float maxPitch = 1.0f;

    // gun shell variables
    public float shotInterval = 0.5f;
    private float shotTimer = 0.0f;
    public float shellVolume = 0.0f;

    void Start()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Shooting();
        Aim();
    }

    // Ray cast mechanic for shooting
    void Shooting()
    {
        // checks if the player is in the alive state which means the player can shoot
        if (health.playerState == PlayerHealth.PlayerState.ALIVE)
        {
            // checks if the controller is connected
            if (xboxController.useController == true)
            {
                // plays the counter
                counter += Time.deltaTime;
                // checks if the player is alive
                if (health.currentHealth > 0)
                {
                    // if the player presses the right trigger
                    if (XCI.GetAxis(XboxAxis.RightTrigger, xboxController.controller) > 0.1f)
                    {
                        // the player will shoot
                        shotTimer += Time.deltaTime;

                        if (counter > delay)
                        {
                            // plays muzzle flash and bullet casing particles
                            muzzleFlash.Play();
                            bulletCasing.Play();

                            // bullets
                            // instantiates bullets as long as the player is holding down the right trigger
                            GameObject bullet = Instantiate(particleProjectile, transform.position, chunkRotation.transform.rotation);
                            // resets the cvounter whenever bullet is copied
                            counter = 0f;
                            // plays the shooting animation while the gun is shooting
                            anim.SetBool("IsAttacking", true);

                            // play audio clips
                            if (shotTimer >= shotInterval)
                            {
                                audioSource.pitch = Random.Range(minPitch, maxPitch);
                                audioSource.PlayOneShot(gunShots[Random.Range(0, gunShots.Count)]);
                                audioSource.PlayOneShot(shellCasings[Random.Range(0, shellCasings.Count)], shellVolume);
                            }

                            // Ray cast mechanic
                            RaycastHit hit;
                            Ray rayCast = new Ray(transform.position, transform.forward);
                            if (Physics.Raycast(rayCast, out hit, range, layerMask))
                            {
                                // enemy health damaged
                                target = hit.transform.GetComponent<EnemyHealth>();
                                if (target != null)
                                {
                                    // damage to the target
                                    target.DamageHealth(damageToGive);
                                    // camera shake when shooting enemies
                                    cameraController.ShakeCamera();
                                    // instantiates blood splatter on enemies that are hit
                                    GameObject impact = Instantiate(bloodEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                                    Destroy(impact, 0.5f);
                                }
                                // destructable pots 
                                DestructableObjects obj = hit.transform.GetComponent<DestructableObjects>();
                                if (obj != null)
                                {
                                    obj.ObjectDamage(damageToGive);
                                    audioSource.pitch = Random.Range(minPitch, maxPitch);
                                    audioSource.PlayOneShot(bulletImpact[Random.Range(0, bulletImpact.Count)]);
                                    // instantiates bullet holes when destructables are shot at
                                    holes = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                                    Destroy(holes, 0.2f);
                                    Debug.DrawLine(transform.position, hit.point, Color.red);
                                }
                            }
                        }
                    }
                    // if player releases the right trigger
                    else if (XCI.GetAxis(XboxAxis.RightTrigger, xboxController.controller) < 0.1)
                    {
                        // stop all particle effects
                        muzzleFlash.Stop();
                        bulletCasing.Stop();
                        shotTimer = 0;
                        // stop anaimation
                        anim.SetBool("IsAttacking", false);
                    }
                }
            }
            // if the player is in revive or dead state make sure the controller is off and doesnt allow the player to shoot
            else if (health.playerState == PlayerHealth.PlayerState.REVIVE && health.playerState == PlayerHealth.PlayerState.DEAD)
            {
                xboxController.useController = false;
            }
        }
    }

    // function for highlighting enemies that are in your range or sight
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