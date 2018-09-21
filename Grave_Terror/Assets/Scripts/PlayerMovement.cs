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
	private bool sprintActive = true;

		Rigidbody playerRigidbody;                                         // Reference to the player's rigidbody.
	Vector3 offset;  
	 private Vector3 movement;                                           // The vector to store the direction of the player's movement.

	 private float currentMovSpeed;

	private void Awake() {
		 // Create a layer mask for the floor layer.
		floorMask = LayerMask.GetMask("Floor");
		 // Set up references.
		playerRigidbody = GetComponent<Rigidbody> ();
	}

    // Physics update only
	private void FixedUpdate() {
		 // 'getaxisraw' creates more responsive movement
		 // Store the input axes.
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
         // Move the player around the scene.
		Move(h, v);
		 // Turn the player to face the mouse cursor.
		Turning();
	}

    #region Movement
	/// <summary> Sets the players movement and normalises it by the speed variable
	/// and by every frame, we then add that movement to the players rigidbody </summary>
	private void Move(float h, float v) {
		movement.Set(h, 0f, v);
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

		movement = movement.normalized * currentMovSpeed * sprintAcceleration * Time.deltaTime;

		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition(transform.position + movement);
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