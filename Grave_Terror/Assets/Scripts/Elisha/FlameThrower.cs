using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class FlameThrower : MonoBehaviour
{
    public XboxController controller;
    public float counter;
    public float delay;
    //public GameObject defaultProjectile;
    public float maxAmmo;
    public float currentAmmo = -1f;
    //public float reloadTime;
    private bool isReloading = false;
    public float coolDown;
    public float coolDownTimer;
    PlayerMovement playerRotation;
    public GameObject sizzleRotation;
    public ParticleSystem flameThrower;

    private void Awake()
    {
        currentAmmo = maxAmmo;
        coolDown = 0.0f;
        playerRotation = GetComponent<PlayerMovement>();
    }

    public void Update()
    {
        
        if (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0.1f)
        {
            currentAmmo--;
            counter += Time.deltaTime;
            if (counter > delay)
            {
                flameThrower.Play();
               // GameObject newFlame = Instantiate(flame, transform.position, sizzleRotation.transform.rotation);
                //flame.SetActive(true);
                counter = 0f;
            }
        }
        else
        {
            flameThrower.Stop();
        }

        if (currentAmmo <= 0f)
        {
            currentAmmo = 0;
            //flame.SetActive(false);

            if (XCI.GetButtonDown(XboxButton.X, controller))
            {
                coolDown = 1.0f;
                currentAmmo = maxAmmo;
                
            }
        }
    }
}