using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterControl;
    private Vector3 offset;
    public float walkSpeed;
    private int floorMask;
    private float camRayLength = 100f;
    public float rotationSmoothing = 7f;
    public float rotationSpeed;
    public float gravity;
    private bool isDodging = false;
    public AnimationCurve dodgeCurve;
    public float dodgeSpeed;
    public float dodgeTime;
    private float dodgeTimer = 0.0f;
    public float trailTime;
    public float trailDamage;
    public GameObject flameTrail;
    public XboxControllerManager xboxController;
    private Camera camRotationY;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 prevRotDirection = Vector3.forward;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        characterControl = GetComponent<CharacterController>();
        camRotationY = GetComponent<Camera>();
    }

    private void Update()
    {
        // If player is dodging
        if (isDodging)
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
            transform.position += transform.forward * dodgeCurve.Evaluate(dodgeTimer / dodgeTime) * dodgeSpeed * Time.deltaTime;
        }
        else
        {
            Move();
        }

        Turning();
    }

    // if a enemy runs into the fire trail
    public void OnTriggerEnter(Collider other)
    {
        // damage the enemy.
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().Ignite(trailDamage);
            print("Damaging enemy " + trailDamage);
        }
    }

    private void Move()
    {
        if (xboxController.useController == true)
        {
            float axisX = XCI.GetAxis(XboxAxis.LeftStickX, xboxController.controller);
            float axisZ = XCI.GetAxis(XboxAxis.LeftStickY, xboxController.controller);
            // free movement 
            moveDirection = new Vector3(axisX * walkSpeed * Time.deltaTime, 0f, axisZ * walkSpeed * Time.deltaTime);
            moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
            //// rotation
            //transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotationSpeed);
            characterControl.Move(moveDirection);
        }
        else if (!xboxController.useController)
        {
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
        if(xboxController.useController)
        {
            //// rotation
            //transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotationSpeed);
            float rotateAxisX = XCI.GetAxis(XboxAxis.RightStickX, xboxController.controller);
            float rotateAxisZ = XCI.GetAxis(XboxAxis.RightStickY, xboxController.controller);

            Vector3 directionVector = new Vector3(rotateAxisX, 0, rotateAxisZ);

            if (directionVector.magnitude < 0.1f)
            {
                directionVector = prevRotDirection;
            }

            directionVector = directionVector.normalized;
            prevRotDirection = directionVector;
            transform.rotation = Quaternion.LookRotation(directionVector);
           

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

                Vector3 position = transform.position + offset;
                // smoothing of the rotation of player
                transform.position = Vector3.Lerp(transform.position, position, rotationSmoothing * Time.deltaTime);
            }
        }
    }
}