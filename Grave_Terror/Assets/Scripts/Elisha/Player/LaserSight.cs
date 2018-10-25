using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class LaserSight : MonoBehaviour
{

    private LineRenderer laser;
    public GameObject GunPoint;

    // Use this for initialization
    void Start()
    {

        laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        laser.SetPosition(0, new Vector3(0, 0, 0));

        RaycastHit hit;
        if (Physics.Raycast(laser.transform.position, laser.transform.forward, out hit))
        {
            if (hit.collider)
            {
                laser.SetPosition(1, new Vector3(0, 0, hit.distance));
            }
        }
        else
        {
            laser.SetPosition(1, new Vector3(0, 0, 5000));
        }
    }
}