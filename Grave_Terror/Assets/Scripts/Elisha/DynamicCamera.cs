using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DynamicCamera : MonoBehaviour {
     
    // list of players
    public List<Transform> players;
    private Camera m_camera;
    // To adjust  the camera
    public Vector3 offset;
    private Vector3 velocity;
    public float smoothTime = 0.5f;
    public float minZoom;
    public float maxZoom;
    public float zoomLimit;

    private void Awake()
    {
        m_camera = GetComponent<Camera>();
    }

    private void Update()
    {
        // if no players on the screen
        if (players.Count == 0)
            return;

        MoveCamera();
        Zoom();
    }  

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistanceX() + GetGreatestDistanceY() / zoomLimit);
        m_camera.fieldOfView = Mathf.Lerp(m_camera.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistanceX()
    {
        var bounds = new Bounds(players[0].position, Vector3.zero);
        for(int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }
        
        return bounds.size.x;
    }

    float GetGreatestDistanceY()
    {
        var bounds = new Bounds(players[0].position, Vector3.zero);
        for (int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }

        return bounds.size.y;
    }

    void MoveCamera()
    {
        // centre point of objects
        Vector3 centrePoint = GetCentrePoint();
        Vector3 newPosition = centrePoint + offset;
        // smooths camera movement
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCentrePoint()
    {
        if (players.Count == 1)
        {
            return players[0].position;
        }

        // Bounds creates a box around the players on the screen and will always centre them
        var bounds = new Bounds(players[0].position, Vector3.zero);
        for(int i = 0; i < players.Count; i++)
        {
            // Encapsulate grows the bounds and includes the new player
            bounds.Encapsulate(players[i].position);
        }

        if (players[0] == null)
        {
            bounds.Encapsulate(players[1].position);
        }
        return bounds.center;
    }
}