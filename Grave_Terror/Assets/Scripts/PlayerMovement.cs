using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] public float walkSpeed;                           // The speed that the player will move at.
	[SerializeField] private int floorMask;                            // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	[SerializeField] private float camRayLength = 100f;                // The length of the ray from the camera into the scene.
	[SerializeField] private float rotationSmoothing = 7f;             // rotation smoothing
	[SerializeField] private float sprintSpeed;
	[SerializeField] private float sprintAcceleration;
	[SerializeField] private float sprintStamina;
	[SerializeField] private float staminaRegeneration;
	public GunControls newGun;
	public GunControls newGun2;
	private bool sprintActive = true;
    Rigidbody playerRigidbody;                                         // Reference to the player's rigidbody.
	Vector3 offset;  
	 //private Vector3 movement;                                           // The vector to store the direction of the player's movement.
	 private float currentMovSpeed;

	private void Awake() {
		 // Create a layer mask for the floor layer.
		floorMask = LayerMask.GetMask("Floor");
		 // Set up references.
		playerRigidbody = GetComponent<Rigidbody> ();
		//playerController = GetComponent<CharacterController>();
	}

    // Physics update only
	private void FixedUpdate() {
		 // 'getaxisraw' creates more responsive movement
		 // Store the input axes.
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
         // Move the player around the scene.
		//Move(h, v);
		 // Turn the player to face the mouse cursor.
		Turning();
	}

	private void Update() {
		Move();
	}

    #region Movement
	/// <summary> Sets the players movement and normalises it by the speed variable
	/// and by every frame, we then add that movement to the players rigidbody </summary>
	private void Move() {
		//movement.Set(h, 0f, v);
		// Sprint 
		if (Input.GetKey(KeyCode.LeftShift) && sprintActive == true) {
			
			currentMovSpeed = sprintSpeed;
			sprintStamina--; 
			// Stamina tank
			if(sprintStamina <= 0f) {
				Debug.Log(sprintStamina);
				sprintStamina = 0f;
				sprintActive = false;
			}
		}
		else {
			currentMovSpeed = walkSpeed;
		}

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
			newGun2.isFiring = true;
		}
		if (Input.GetKeyUp(KeyCode.Mouse0)) {
			newGun.isFiring = false;
			newGun2.isFiring = false;
		}
		
		//movement = movement.normalized * currentMovSpeed * sprintAcceleration * Time.deltaTime;

		// Move the player to it's current position plus the movement.
		//playerRigidbody.MovePosition(transform.position + movement);
	}
	#endregion

    #region Rotation / rayCast
	/// <summary> We first create a variable called camRay that finds the mouses 
	/// position on the screen, we then create an if statement which checks if the 
	/// camera has found the camRay and if it has we want it to send that information 
	/// back to the floorHit variable.If the statement is true we then change the 
	/// players rotation based off of the mouses position </summary>
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
	}
	#endregion
}