using UnityEngine;
using XboxCtrlrInput;


public class FlameGaunletLeft : MonoBehaviour {

    PlayerMovement playerRotation;
    public GameObject sizzleRotation;
    public float counter;
    public float delay;
    public GameObject flameBallLeft;
    public XboxControllerManager xboxController;
    public Animator anim;

    private void Awake()
    {
        playerRotation = GetComponent<PlayerMovement>();
    }

    public void Update()
    {
        counter += Time.deltaTime;
        if (XCI.GetAxis(XboxAxis.LeftTrigger, xboxController.controller) > 0.1f)
        {
            if (counter > delay)
            {
                GameObject newFlame = Instantiate(flameBallLeft, transform.position, sizzleRotation.transform.rotation);
                anim.SetBool("IsAttacking", true);
                counter = 0.0f;
            }
        }
        else if (XCI.GetAxis(XboxAxis.LeftTrigger, xboxController.controller) < 0.1f)
        {
            anim.SetBool("IsAttacking", false);
        }
    }
}