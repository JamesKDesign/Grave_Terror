using UnityEngine;
using XboxCtrlrInput;

public class FlameThrower : MonoBehaviour
{
    PlayerMovement playerRotation;
    public GameObject sizzleRotation;
    public XboxController controller;
    public float counter;
    public float delay;
    public GameObject flameBall;

    private void Awake()
    {
        playerRotation = GetComponent<PlayerMovement>();
    }

    public void Update()
    {
        counter += Time.deltaTime;
        if (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0.1f)
        {
            if (counter > delay)
            {
                GameObject newFlame = Instantiate(flameBall, transform.position, sizzleRotation.transform.rotation);
                counter = 0.0f;
            } 
        }
        else if (XCI.GetAxis(XboxAxis.RightTrigger, controller) < 0.1f)
        {
            
            print("flame has stopped");
        }
    }
}