using UnityEngine;

public class FlameMovement : MonoBehaviour {

    public float Speed;
    public float flameLife;
    public float flameDamage;
    public float damageTimer;

    // Update is called once per frame
    void FixedUpdate()
    {
        // changing the objects transform by multiplying the vector3 forward by speed and deltatime
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        flameLife -= Time.deltaTime;
        if (flameLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().Ignite(flameDamage);
            print("Damaging enemy " + flameDamage);
        }
    }
}