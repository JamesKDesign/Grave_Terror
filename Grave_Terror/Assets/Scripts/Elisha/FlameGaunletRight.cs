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

    private void Awake()
    {
        playerRotation = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        FlameShootingRight();
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
}