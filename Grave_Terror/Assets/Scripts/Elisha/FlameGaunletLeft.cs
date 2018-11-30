// Author: Elisha Anagnostakis
// Date Modified: 20/11/18
// Purpose: This script handles sizzles left flame thrower. The shooting of the flame mechanics.

using UnityEngine;
using XboxCtrlrInput;
public class FlameGaunletLeft : MonoBehaviour {

    // references
    PlayerMovement playerRotation;
    public XboxControllerManager xboxController;
    public Animator anim;
    public ScoreBoard score;
    public PlayerHealth health;

    // game objects components
    public GameObject flameBallLeft;
    public GameObject sizzleRotation;
    public GameObject newFlame;

    // player targeting enemies location
    Vector3 hitLocation = Vector3.zero;

    // audio
    public float counter;
    public float delay;
	public float minPitch = 0.0f;
	public float maxPitch = 1.0f;
	private AudioSource audioSource;
	public AudioClip flameAudio;

    private void Awake()
    {
		audioSource = GetComponent<AudioSource>();

        playerRotation = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        FlameShootingLeft();
        Aim();
    }

    public void FlameShootingLeft()
    {
        // counter starts counting
        counter += Time.deltaTime;
        // checks if player is alive so they can shoot
        if(health.playerState == PlayerHealth.PlayerState.ALIVE)
        {
            // if player presses the left trigger 
            if (XCI.GetAxis(XboxAxis.LeftTrigger, xboxController.controller) > 0.1f)
            {
                // sizzle will shoot her left gauntlet
                if (counter > delay)
                {
                    // instantiates the flame at the 
                    newFlame = Instantiate(flameBallLeft, transform.position, sizzleRotation.transform.rotation);
                    anim.SetBool("IsAttacking", true);
					audioSource.pitch = Random.Range(minPitch, maxPitch);
					audioSource.PlayOneShot(flameAudio);

                    counter = 0.0f;
                }
            }
            else if (XCI.GetAxis(XboxAxis.LeftTrigger, xboxController.controller) < 0.1f)
            {
                anim.SetBool("IsAttacking", false);
            }
        }
        else if(health.playerState == PlayerHealth.PlayerState.REVIVE && health.playerState == PlayerHealth.PlayerState.DEAD)
        {
            xboxController.useController = false;
        }
    }

    void Aim()
    {
        //Raycasting from the player's position and creating an array of hit results from players forward direction
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position, transform.forward, 100.0f);
        Debug.DrawRay(transform.position, transform.forward, Color.magenta);
        foreach (RaycastHit result in hit)
        {
            //If the cast hits an object tagged as 'enemy', run settargeted function on Enemy script
            if (result.collider.gameObject.tag == "Enemy")
            {
                Debug.Log("Enemy Targeted");
                hitLocation = result.point;
                result.collider.gameObject.GetComponent<Enemy>().SetTargeted();

                break;
            }
        }
    }

    //Debug cube on targeted location
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(hitLocation, Vector3.one);
    }
}