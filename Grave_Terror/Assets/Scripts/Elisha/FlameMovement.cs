using UnityEngine;

public class FlameMovement : MonoBehaviour {

    public float Speed;
    public float flameLife;
    public float flameDamage;
    private GameObject scoreboard;

    private void Start()
    {
        scoreboard = GameObject.FindGameObjectWithTag("Canvas");
    }

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
            if (other.gameObject.GetComponent<Enemy>().state == E_STATE.NORMAL)
            {
                scoreboard.GetComponent<ScoreBoard>().SizzleScore += 1;
                scoreboard.GetComponent<ScoreBoard>().Sizzle.text = scoreboard.GetComponent<ScoreBoard>().SizzleScore.ToString();
            }
            other.gameObject.GetComponent<Enemy>().Ignite(flameDamage);
        }
        else if (other.gameObject)
        {
            Destroy(gameObject);
        }
    }
}