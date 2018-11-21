using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform camTransform;

    public float shakeDuration = 0.1f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    bool doShake = false;

    // Use this for initialization
    void Start () {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    public void ShakeCamera()
    {
        doShake = true;
    }

    // Update is called once per frame
    void Update () {
        if (doShake == true)
        {
            if (shakeDuration > 0)
            {
                camTransform.localPosition = camTransform.position + Random.insideUnitSphere * shakeAmount;
                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = 0.1f;
                doShake = false;
            }
        }
    }
}
