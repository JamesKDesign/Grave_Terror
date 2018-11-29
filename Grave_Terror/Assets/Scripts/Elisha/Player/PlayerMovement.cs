using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerMovement : MonoBehaviour
{
    private float camRayLength = 100f;
    public float rotationSmoothing = 7f;
    public float rotationSpeed;
    public float gravity;
    //private bool isDodging = false;
    //public AnimationCurve dodgeCurve;
    //public float dodgeSpeed;
    //public float dodgeTime;
    //private float dodgeTimer = 0.0f;
    //public float trailTime;
    //public float trailDamage;
    //public GameObject flameTrail;
    public XboxControllerManager xboxController;
    private Vector3 prevRotDirection = Vector3.forward;
    public float walkSpeed;
    public float maxSpeed;

    // Relative camera rotation
    public Transform cam;
    private Vector3 inputDirection = new Vector3(0, 0, 0);
    private Vector3 moveDirection = new Vector3(0, 0, 0);
    private Vector3 directionVector = new Vector3(0, 0, 0);
    private CharacterController characterControl;

    // Animation
    public Animator anim;

    //Audio
    public List<AudioClip> footSteps = new List<AudioClip>();
    private AudioSource audioSource;
    public float stepInterval = 0.5f;
    private float stepTimer = 0.0f;
    public float minPitch = 0.0f;
    public float maxPitch = 1.0f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        characterControl = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    //public void Dashing()
    //{
    //    // If player is dodging
    //    if (isDodging)
    //    {

    //        // start dodge and fire trail timers
    //        dodgeTimer += Time.deltaTime;
    //        if (dodgeTimer >= dodgeTime)
    //        {
    //            isDodging = false;
    //            anim.SetBool("IsDashing", false);
    //            flameTrail.GetComponent<ParticleSystem>().Stop();
    //            dodgeTimer = 0.0f;
    //        }

    //        transform.position += moveDirection.normalized * dodgeSpeed * Time.deltaTime;
    //        moveDirection.y = 0;
    //    }
    //    else
    //    {
    //        Move();
    //    }

    //    if (XCI.GetButtonDown(XboxButton.LeftStick, xboxController.controller))
    //    {
    //        isDodging = true;
    //        anim.SetBool("IsDashing", true);
    //        flameTrail.GetComponentInChildren<ParticleSystem>().Play();
    //    }
    //}

    // if a enemy runs into the fire trail
    //public void OnTriggerEnter(Collider other)
    //{
    //    if(isDodging)
    //    {
    //        // damage the enemy.
    //        if (other.gameObject.tag == "Enemy")
    //        {
    //            other.gameObject.GetComponent<Enemy>().Ignite(trailDamage);
    //            print("Damaging enemy " + trailDamage);
    //        }
    //    }
    //    else
    //    {
    //        isDodging = false;
    //    }
    //}

    public void Move()
    {
        if (xboxController.useController == true)
        {

            float axisX = XCI.GetAxisRaw(XboxAxis.LeftStickX, xboxController.controller);
            float axisZ = XCI.GetAxisRaw(XboxAxis.LeftStickY, xboxController.controller);
            Quaternion inputRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            Vector3 animDirection = inputRotation * (Vector3.Scale(cam.eulerAngles, new Vector3(0,1,0)) + new Vector3(axisX, 0, axisZ));
            
            Debug.DrawLine(transform.position, transform.position + animDirection * 2.0f, Color.magenta);

            Debug.Log(transform.eulerAngles.y);
            //Debug.Log(animDirection);
            if (transform.eulerAngles.y > 60.0f && transform.eulerAngles.y < 120.0f)
                animDirection = -animDirection;
            if (transform.eulerAngles.y > 230.0f && transform.eulerAngles.y < 330.0f)
                animDirection = -animDirection;

            anim.SetFloat("DirectionX", animDirection.x);
            anim.SetFloat("DirectionY", animDirection.z);

            inputDirection = new Vector3(axisX, 0, axisZ);
            Vector3 camForward = cam.forward;
            camForward.y = 0;
            camForward = camForward.normalized;

            Quaternion cameraRotation = Quaternion.FromToRotation(Vector3.forward, camForward);
            Vector3 lookForward = cameraRotation * inputDirection;

            if (inputDirection.sqrMagnitude > 0)
            {
                anim.SetBool("IsMoving", true);

                Debug.Log("IsMoving = True");
                Ray look = new Ray(transform.position, lookForward);
                transform.LookAt(look.GetPoint(1));

                stepTimer += Time.deltaTime;
                
            }
            else
            {
                anim.SetBool("IsMoving", false);
                Debug.Log("IsMoving = false");
            }


            moveDirection = transform.forward * walkSpeed * inputDirection.sqrMagnitude;

            if(walkSpeed >= maxSpeed)
            {
                walkSpeed = maxSpeed;
            }

            moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
            characterControl.Move(moveDirection * Time.deltaTime);

            if (inputDirection.sqrMagnitude > 0.1)
            {
                if (stepTimer >= stepInterval)
                {
                    audioSource.pitch = Random.Range(minPitch, maxPitch);
                    audioSource.PlayOneShot(footSteps[Random.Range(0, footSteps.Count)]);
                    stepTimer = 0;

                }
            }
        }
    }

    // Rotation of player
    public void Turning()
    {
        if(xboxController.useController)
        {
            float rotateAxisX = XCI.GetAxisRaw(XboxAxis.RightStickX, xboxController.controller);
            float rotateAxisZ = XCI.GetAxisRaw(XboxAxis.RightStickY, xboxController.controller);

            directionVector = new Vector3(rotateAxisX, 0, rotateAxisZ);

            if (directionVector.magnitude < 0.1f)
            {
                directionVector = prevRotDirection;
            }

            directionVector = directionVector.normalized;
            prevRotDirection = directionVector;
            transform.localRotation = Quaternion.LookRotation(directionVector);
        }
    }
}