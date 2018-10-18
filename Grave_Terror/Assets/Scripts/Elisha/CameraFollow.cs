using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Camera camera;
    // time for the camera to refocus
    [SerializeField] private float DampTime;
    // Space between the top/bottom most target and the screen edge
    [SerializeField] private float ScreenBuffer;              
    [SerializeField] private float MinSize;
    // All the targets the camera needs to have within its view
    [SerializeField] private Transform[] Targets;             
    [SerializeField] private float ZoomSpeed;                 
    [SerializeField] private Vector3 MoveVelocity;
    // The position the camera is moving towards based off the players movements
    private Vector3 DesiredPosition;                          

    // Awake initialises any variables before the game actually starts
    private void Awake ()
    {
		// Camera is child to CameraRig 
        camera = gameObject.GetComponentInChildren<Camera>();
        SetStartPositionAndSize();
    }

    // FixedUpdate runs functions or variwables that are associated with physics only
    private void Update ()
    {
        // func call moves the camera towards the desired position
        Move ();
        
        // func call changes the size of the camera based off the players movements in the world
        Zoom ();
    }

    // Camera move logic
    private void Move ()
    {
        // Finds the average position of the targets to then move accordingly 
        FindAveragePosition ();

        // Smoothly transitions to that position *('ref' references back to m_MoveVelocity)*
        transform.position = Vector3.SmoothDamp(transform.position, DesiredPosition, ref MoveVelocity, DampTime);
    }

	// Finds average position of both players
    private void FindAveragePosition ()
    {
		// Creates a empty new vector3
        Vector3 averagePos = new Vector3 ();
		// How many targets can the camera see
        int numTargets = 0;

        // Go through all the targets and add their positions together
        for (int i = 0; i < Targets.Length; i++)
        {
            // If the target isn't active, go on to the next one
            if (!Targets[i].gameObject.activeSelf)
                continue;

            // Add to the average and increment the number of targets in the average
            averagePos += Targets[i].position;
            numTargets++;
        }

        // If there are targets divide the sum of the positions by the number of them to find the average
        if (numTargets > 0)
            averagePos /= numTargets;

        // Keep the same y value
        averagePos.y = transform.position.y;

        // The desired position is the average position
        DesiredPosition = averagePos;
    }

    // Camera zoom in and out
    private void Zoom ()
    {
        // Find the required size based on the desired position and smoothly transition to that size
        float requiredSize = FindRequiredSize();
        camera.fieldOfView = Mathf.SmoothDamp (camera.fieldOfView, requiredSize, ref ZoomSpeed, DampTime);
    }

    // Finds the size at which the camera distance should be
    private float FindRequiredSize ()
    {
        // Find the position the camera rig is moving towards in its local space
        Vector3 desiredLocalPos = transform.InverseTransformPoint(DesiredPosition);

        // Start the camera's size calculation at zero
        float size = 6.5f;

        // Go through all the targets
        for (int i = 0; i < Targets.Length; i++)
        {
            // ... and if they aren't active continue on to the next target
            if (!Targets[i].gameObject.activeSelf)
                continue;

            // Otherwise, find the position of the target in the camera's local space
            Vector3 targetLocalPos = transform.InverseTransformPoint(Targets[i].position);

            // Find the position of the target from the desired position of the camera's local space
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            // Choose the largest out of the current size and the distance of the tank 'up' or 'down' from the camera
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            // Choose the largest out of the current size and the calculated size based on the tank being to the left or right of the camera
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / camera.aspect);
        }

        Debug.Log(size);
        // Add the edge buffer to the size
        size += ScreenBuffer;

        Debug.Log(MinSize);

        // Make sure the camera's size isn't below the minimum
        size = Mathf.Max (size, MinSize);

        return size;
    }

	
    // Camera start position
    public void SetStartPositionAndSize ()
    {
        // Find the desired position
        FindAveragePosition ();

        // Set the camera's position to the desired position without damping
        transform.position = DesiredPosition;

        // Find and set the required size of the camera
        camera.fieldOfView = FindRequiredSize();
    }
}