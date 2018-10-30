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

    public float maxAmmo;
    public float currentAmmo = -1f;
    //public float reloadTime;
    private bool isReloading = false;
    public float coolDown;
    public float coolDownTimer;

    private void Awake()
    {
        currentAmmo = maxAmmo;
        coolDown = 0.0f;
    }

    public void Update()
    {
        
        if (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0.1f)
        {
            currentAmmo--;
            counter += Time.deltaTime;
            if (counter > delay)
            {
                // bullets
                GameObject newFlame = Instantiate(flame, transform.position, transform.rotation);
                flame.SetActive(true);
                counter = 0f;
            }
        }

        if (currentAmmo <= 0f)
        {
            currentAmmo = 0;
            flame.SetActive(false);

            if (XCI.GetButtonDown(XboxButton.X, controller))
            {
                coolDown = 1.0f;
                currentAmmo = maxAmmo;
                //Reloading();
            }
        }
    }

    //public void Reloading()
    //{
    //    isReloading = true;
    //    coolDown += Time.deltaTime;
    //    if (coolDown > coolDownTimer)
    //    {
    //        coolDown = 0.0f;
    //        currentAmmo = maxAmmo;
    //        isReloading = false;
    //    }
    //}
}