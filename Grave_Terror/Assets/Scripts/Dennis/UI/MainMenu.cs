using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MENU_STATE
{
	MENU,
	//OPTIONS,
	QUIT,
	CHARACTER_SELECT,
	START_GAME,
}

[System.Serializable]
public class Rails
{
	[Tooltip("For visualization help")]
	public Color color;
	public Transform[] pos;
	public float travelTime;
}

[System.Serializable]
public class ValidPath
{
	public int from; //Where we are going from
	public int to; //The point we want to go to
}

public class WATransform
{
	public Vector3 pos;
	public Quaternion rot;
}

public class MainMenu : MonoBehaviour
{
	Camera mainCamera;
	[Tooltip("Transforms that the camera will travel between\nAffected by rotation!")]
	public Rails[] cameraPath;
	public ValidPath[] paths;

	public MENU_STATE state = MENU_STATE.MENU;

	//We start at nothing and follow the entrance path
	private int current = -1;

	//Is the camera in motion?
	private bool moving = false;
	//Timer for the camera in motion
	private float timer = 0.0f;
	//The target path we are taking
	private int target = 0;
	private int sequence = 0;

	// Use this for initialization
	void Start ()
	{
		mainCamera = Camera.main;

		MoveTo(0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//If we want to move the camera somewhere
		if (moving)
		{
			timer += Time.deltaTime / cameraPath[target].travelTime;
			//Break on timeout
			if (timer > 1.0f)
			{
				moving = false;
				current = target;
			}
			//Lerp rotation and position between points
			else
			{
				WATransform[] pos = new WATransform[cameraPath[target].pos.Length];
				for (int i = 0; i < cameraPath[target].pos.Length; i++)
				{
					pos[i] = new WATransform();
					pos[i].pos = cameraPath[target].pos[i].position;
					pos[i].rot = cameraPath[target].pos[i].localRotation;
				}
				WATransform output = PositionLerpRecursive(pos, timer);
				mainCamera.transform.position = output.pos;
				mainCamera.transform.localRotation = output.rot;
			}
		}
	}

	//Checks if the target has a valid path to it
	public bool MoveTo(int _target)
	{
		



		moving = true;
		timer = 0.0f;
		target = _target;
		sequence = 0;

		return true;
	}

	//Gizmos to help visualize it
	private void OnDrawGizmos()
	{
		foreach (Rails r in cameraPath)
		{
			Gizmos.color = r.color;
			for (int i = 0; i < r.pos.Length - 1; ++i)
			{
				Gizmos.DrawLine(r.pos[i].position, r.pos[i + 1].position);
			}
			Gizmos.DrawSphere(r.pos[r.pos.Length - 1].position, 0.2f);
		}
	}

	//Recursive lerp to guide the camera smoothly along its rails
	private WATransform PositionLerpRecursive(WATransform[] _pos, float _time)
	{
		if (_pos.Length == 1)
			return _pos[0];
		//Create a new array thats 1 shorter then the original
		WATransform[] pos = new WATransform[_pos.Length - 1];
		//Fill it
		for (int i = 0; i < pos.Length; ++i)
		{
			WATransform temp = new WATransform();
			temp.pos = Vector3.Lerp(_pos[i].pos, _pos[i + 1].pos, _time);
			temp.rot = Quaternion.Lerp(_pos[i].rot, _pos[i + 1].rot, _time);
			pos[i] = temp;
		}
		//Next iter
		return PositionLerpRecursive(pos, _time);
	}
}
