using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    public float Speed;

	// Update is called once per frame
	void Update () {
		BulletFire();
	}

    // The bullets movement 
	private void BulletFire() {

		// changing the objects transform by multiplying the vector3 forward by speed and deltatime
		transform.Translate(Vector3.forward * Speed * Time.deltaTime);
		}
	}