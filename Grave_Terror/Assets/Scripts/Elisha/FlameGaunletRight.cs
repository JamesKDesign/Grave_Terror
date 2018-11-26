using UnityEngine;
using XboxCtrlrInput;

public class FlameGaunletRight : MonoBehaviour
{
    PlayerMovement playerRotation;
    public GameObject sizzleRotation;
    public float counter;
    public float delay;
    public GameObject flameBallRight;
    public XboxControllerManager xboxController;
    public Animator anim;
    public PlayerHealth health;
    private Vector3 hitLocation;

    private void Awake()
    {
        playerRotation = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        FlameShootingRight();
        Aim();
    }

    public void FlameShootingRight()
    {
        if(health.playerState == PlayerHealth.PlayerState.ALIVE)
        {
            counter += Time.deltaTime;
            if (XCI.GetAxis(XboxAxis.RightTrigger, xboxController.controller) > 0.1f)
            {
                if (counter > delay)
                {
                    GameObject newFlame = Instantiate(flameBallRight, transform.position, sizzleRotation.transform.rotation);
                    anim.SetBool("IsAttacking", true);
                    counter = 0.0f;
                }
            }
            else if (XCI.GetAxis(XboxAxis.RightTrigger, xboxController.controller) < 0.1f)
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