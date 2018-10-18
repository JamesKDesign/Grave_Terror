using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    public float Speed;
    public GameObject enemy;

	// Update is called once per frame
	void Update () {
		BulletFire();
	}

    // The bullets movement 
	private void BulletFire() {

		// changing the objects transform by multiplying the vector3 forward by speed and deltatime
		transform.Translate(Vector3.forward * Speed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider collide)
    {
        //if(collide.gameObject.tag == "Enemy")
        //{
        //    enemy = collide.gameObject;
        //    ScoreManager.score += 1;
        //    Destroy(enemy.gameObject);
        //}
        //else
        //{
        //    Debug.Log("Collision failed");
        //}
    }
}