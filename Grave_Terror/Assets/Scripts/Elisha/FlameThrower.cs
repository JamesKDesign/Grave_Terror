using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class FlameThrower : MonoBehaviour
{
    public GameObject flame;
    public XboxController controller;
    public float counter;
    public float delay;

    public void Update()
    {
        if (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0.1f)
        {
            counter += Time.deltaTime;
            if (counter > delay)
            {
                // bullets
                GameObject newFlame = Instantiate(flame, transform.position, transform.rotation);
                counter = 0f;
            }
        }
    }
}
