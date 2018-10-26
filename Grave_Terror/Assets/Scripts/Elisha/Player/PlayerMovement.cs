using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] public float walkSpeed;
    [SerializeField] private int floorMask;
    [SerializeField] private float camRayLength = 100f;
    [SerializeField] private float rotationSmoothing = 7f;
    Rigidbody playerRigidbody;
    private Vector3 previousRotation = Vector3.forward;
    Vector3 offset;
    public XboxControllerManager xboxController;

    bool isDodging = false;
    public AnimationCurve dodgeCurve;
    public float dodgeSpeed;
    public float dodgeTime;
    private float dodgeTimer = 0.0f;

    public GameObject flameTrail;
    //private float trailTimer = 0.0f;
    public float trailTime;
    public float trailDamage;

    private Camera camRotationY;

    private void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");
        // Set up references.
        playerRigidbody = GetComponent<Rigidbody>();
        /*flameTrail = GetComponent<ParticleSystem>()*/;
        camRotationY = GetComponent<Camera>();
    }

    // Physics update only
    private void FixedUpdate()
    {
        // Turn the player to face the mouse cursor.
        Turning();
    }

    private void Update()
    {
        // If player is dodging
        if(isDodging)
        {
            // start dodge and fire trail timers
            dodgeTimer += Time.deltaTime;
            if (dodgeTimer >= dodgeTime)
            {
                isDodging = false;
                dodgeTimer = 0.0f;
            }
            GameObject trail = Instantiate(flameTrail);
            trail.transform.parent = this.transform;
            trail.transform.localPosition = Vector3.zero;
            Destroy(trail, trailTime);
            transform.position += transform.forward * dodgeCurve.Evaluate(dodgeTimer/dodgeTime) * dodgeSpeed * Time.deltaTime;
            
        }

        else
        {
            Move();
        }
    }

    // if a enemy rins into the fire trail
    public void OnTriggerEnter(Collider other)
    {
        // damage the enemy.
        if (other.gameObject.tag == "Fire")
        {
            other.gameObject.GetComponent<Enemy>().Ignite(trailDamage);
            print("Damaging enemy " + trailDamage);
        }
    }

    // Basic movement of the player
    private void Move()
    {

        if (xboxController.useController == true)
        {
            float axisX = XCI.GetAxis(XboxAxis.LeftStickX, xboxController.controller);
            float axisZ = XCI.GetAxis(XboxAxis.LeftStickY, xboxController.controller);

            

            transform.position += new Vector3(axisX * walkSpeed * Time.deltaTime, 0, axisZ * walkSpeed * Time.deltaTime);
        }
        else if (!xboxController.useController)
        {
            // Basic player movement (No physics)
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(walkSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(0, 0, walkSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += new Vector3(-walkSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(0, 0, -walkSpeed * Time.deltaTime);
            }
        }

        if (XCI.GetButtonDown(XboxButton.LeftStick, xboxController.controller))
        {
            isDodging = true;
        }
    }

    // Rotation of player
    private void Turning()
    {

        if (xboxController.useController == true)
        {
            float rotateAxisX = XCI.GetAxis(XboxAxis.RightStickX, xboxController.controller);
            float rotateAxisZ = XCI.GetAxis(XboxAxis.RightStickY, xboxController.controller);

            Vector3 direction = new Vector3(rotateAxisX, 0, rotateAxisZ);

            if (direction.magnitude < 0.1f)
            {
                direction = previousRotation;
            }

            direction = direction.normalized;
            previousRotation = direction;
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else if (!xboxController.useController)
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0f;

                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                playerRigidbody.MoveRotation(newRotation);

                Vector3 position = transform.position + offset;
                // smoothing of the rotation of player
                transform.position = Vector3.Lerp(transform.position, position, rotationSmoothing * Time.deltaTime);
            }
        }
    }
}