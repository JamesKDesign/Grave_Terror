using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnFire : MonoBehaviour {

    public ParticleSystem setOnFire;

	// Use this for initialization
	void Awake () {
        setOnFire = GetComponent<ParticleSystem>();
        setOnFire.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Fire")
        {
            setOnFire.Play();
        }
    }
}
