using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] public float walkSpeed;
    [SerializeField] private int floorMask;
    [SerializeField] private float camRayLength = 100f;
    [SerializeField] private float rotationSmoothing = 7f;
    Rigidbody playerRigidbody;
    public XboxController controller;
    public bool useController = false;
    private Vector3 previousRotation = Vector3.forward;
    Vector3 offset;
    CharacterController characterController;

    private void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");
        // Set up references.
        playerRigidbody = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();

    }

    // Physics update only
    private void FixedUpdate()
    {
        // Turn the player to face the mouse cursor.
        Turning();
    }

    private void Update()
    {
        Move();
    }

    // Basic movement of the player
    private void Move()
    {

        if (useController == true)
        {
            float axisX = XCI.GetAxis(XboxAxis.LeftStickX, controller);
            float axisZ = XCI.GetAxis(XboxAxis.LeftStickY, controller);

            transform.position += new Vector3(axisX * walkSpeed * Time.deltaTime, 0, axisZ * walkSpeed * Time.deltaTime);
        }
        else if (!useController)
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
    }

    // Rotation of player
    private void Turning()
    {

        if (useController == true)
        {
            float rotateAxisX = XCI.GetAxis(XboxAxis.RightStickX, controller);
            float rotateAxisZ = XCI.GetAxis(XboxAxis.RightStickY, controller);

            Vector3 direction = new Vector3(rotateAxisX, 0, rotateAxisZ);

            if (direction.magnitude < 0.1f)
            {
                direction = previousRotation;
            }

            direction = direction.normalized;
            previousRotation = direction;
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else if (!useController)
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