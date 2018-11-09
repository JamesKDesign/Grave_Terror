using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class FlameThrower : MonoBehaviour
{
    PlayerMovement playerRotation;
    public GameObject sizzleRotation;
    public GameObject flame;
    public XboxController controller;
    public float counter;
    public float delay;

    private void Awake()
    {
        playerRotation = GetComponent<PlayerMovement>();
    }

    public void Update()
    {
        
        if (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0.1f)
        {
            counter += Time.deltaTime;
            GameObject newFlame = Instantiate(flame, transform.position, sizzleRotation.transform.rotation);
           
        }
    }
}