using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnFire : MonoBehaviour {

    public ParticleSystem setOnFire;
    private float timer;

	// Use this for initialization
	void Awake () {
        setOnFire = GetComponentInChildren<ParticleSystem>();
        timer = 0.0f;
        //setOnFire.Stop();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 4.0f)
        {
            timer = 0.0f;
            
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Fire")
        {
            timer = 0.0f;
            setOnFire.Play();
        }
        
    }
}