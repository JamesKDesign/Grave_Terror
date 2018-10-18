﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] public float walkSpeed;
    [SerializeField] private int floorMask;
    [SerializeField] private float camRayLength = 100f;
    [SerializeField] private float rotationSmoothing = 7f;             
    Rigidbody playerRigidbody;                                         
	Vector3 offset;  
	public GunControls newGun;
	public GunControls newGun2;

	private void Awake() {
		 // Create a layer mask for the floor layer.
		floorMask = LayerMask.GetMask("Floor");
		 // Set up references.
		playerRigidbody = GetComponent<Rigidbody> ();
	}

    // Physics update only
	private void FixedUpdate() {    
	 // Turn the player to face the mouse cursor.
		Turning();
	}

	private void Update() {
		Move();
	}

	// Sets the players movement and normalises it by the speed variable and by every frame, we then add that movement to the players rigidbody 
	private void Move() {

		// Basic player movement (No physics)
		if (Input.GetKey(KeyCode.W)) {
			transform.position += new Vector3( walkSpeed * Time.deltaTime, 0, 0);
		}
		if (Input.GetKey(KeyCode.A)) { 
			transform.position += new Vector3( 0, 0, walkSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.S)) {
			transform.position += new Vector3( -walkSpeed * Time.deltaTime, 0, 0);
		}
		if (Input.GetKey(KeyCode.D)) {
			transform.position += new Vector3(0, 0, - walkSpeed * Time.deltaTime);
		}

        // firing Machine Gun 
		if (Input.GetKey(KeyCode.Mouse0)) {
			newGun.isFiring = true;
			//newGun2.isFiring = true;
		}
		if (Input.GetKeyUp(KeyCode.Mouse0)) {
			newGun.isFiring = false;
			//newGun2.isFiring = false;
		}
	}

    // ray casting
	private void Turning() {
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;

		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
			

			Vector3 position = transform.position + offset;
			// smoothing of the rotation of player
			transform.position = Vector3.Lerp(transform.position, position, rotationSmoothing * Time.deltaTime);
		}
        else
        {
            Debug.Log("Turning is not working");
        }
	}
}