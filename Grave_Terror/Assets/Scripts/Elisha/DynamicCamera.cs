// Author: Elisha Anagnostakis
// Date Modified: 20/11/18
// Purpose: This script is for the camera which gets the camera to follow both players around thew world and making sure the players cannot walk out of the screen
// it sets a bounding box around the amount of players that is on the screen and finds the greatest distance between them and zooms in and out according to where the players walk

using System.Collections.Generic;
using UnityEngine;

// will always require the camera component at all times
[RequireComponent(typeof(Camera))]
public class DynamicCamera : MonoBehaviour {
     
    // list of players
    public List<Transform> players;

    // To adjust  the cameras positioning
    private Camera m_camera;
    public Vector3 offset;
    private Vector3 velocity;

    // cameras zoom variables and smooth variables 
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
            // then do nothing
            return;

        MoveCamera();
        Zoom();
    }  

    // zooms in and out according to the players positioning
    void Zoom()
    {
        // lerps between the greatest distance of x and y
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistanceX() + GetGreatestDistanceY() / zoomLimit);
        // warps the cameras field of view to make sure the players dont walk off the screen
        m_camera.fieldOfView = Mathf.Lerp(m_camera.fieldOfView, newZoom, Time.deltaTime);
    }

    // finds the greatest distance on the x axis of the map
    float GetGreatestDistanceX()
    {
        // creates a bounding box around the player
        var bounds = new Bounds(players[0].position, Vector3.zero);
        for(int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }
        
        return bounds.size.x;
    }

    // finds the greatest distance on the y axis of the map
    float GetGreatestDistanceY()
    {
        // creates a bounding box around the player
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