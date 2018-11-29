using UnityEngine;

public class ObjectOnFire : MonoBehaviour {

    public GameObject onFireEffect;
    private float timer;
    public float burnTime = 4.0f; 

	// Use this for initialization
	void Awake () {
        timer = 0.0f;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if(timer > burnTime)
        {
            timer = 0.0f;
            onFireEffect.GetComponentInChildren<ParticleSystem>().Stop();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Fire")
        {
            timer = 0.0f;
            onFireEffect.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}