using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Transform target;
    private float pendingShake;
    [Range( 0, 5f)]
    public float intensity;
    Vector3 initialPosition;
    bool isShaking = false;

    private void Awake()
    {
        target = GetComponent<Transform>();
        // relative to the parents position
        initialPosition = target.localPosition;
    }

    public void Shake(float duration)
    {
        if (duration > 0)
        {
            pendingShake += duration;
        }
    }

    private void Update()
    {
        if (pendingShake > 0 && !isShaking)
        {
            StartCoroutine(DoShake());
        }
    }

    IEnumerator DoShake()
    {
        isShaking = true;

        var startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startTime + pendingShake)
        {
            var randomPoint = new Vector3(Random.Range(-1f, 1f) * intensity, Random.Range(-1f, 1f) * intensity, initialPosition.x);
            target.localPosition = randomPoint;
            yield return null;
        }

        pendingShake = 0f;
        target.localPosition = initialPosition;
        isShaking = false;
    }
}
