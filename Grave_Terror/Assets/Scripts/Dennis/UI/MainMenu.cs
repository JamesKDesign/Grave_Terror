using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
	[Tooltip("Element no. of desired start")]
	public int startPoint;
	[Tooltip("Element no. of desired destination")]
	public int endPoint;
	public float travelTime;
}

[System.Serializable]
public class Points
{
	public Transform transform;
	public UnityEvent call;
}

//Class because you cant make instances of transforms to use for storage
public class WATransform
{
	public Vector3 pos;
	public Quaternion rot;
}

public class MainMenu : MonoBehaviour
{
	Camera mainCamera;

	public string gameScene;

	[Header("Camera")]
	public Points[] endPoints;
	[Tooltip("Transforms that the camera will travel between\nAffected by rotation!")]
	public Rails[] cameraPath;

	public MENU_STATE state = MENU_STATE.MENU;

	//We start at nothing and follow the entrance path
	private int current = -1;

	//Is the camera in motion?
	private bool moving = false;
	//Timer for the camera in motion
	private float timer = 0.0f;
	//The target path we are taking
	public int path = 0;
	//If we are following it in reverse order
	private bool reverse = false;

	// Use this for initialization
	void Start ()
	{
		mainCamera = Camera.main;

		ForceMoveTo(0);
		//MoveTo(1);
	}
	
	// Update is called once per frame
	void Update ()
	{

		//If we want to move the camera somewhere
		if (moving)
		{
			if (reverse)
			{
				timer -= Time.deltaTime / cameraPath[path].travelTime;
				//Break on timeout
				if (timer < 0.0f)
				{
					moving = false;
					current = cameraPath[path].startPoint;
				}
			}
			else
			{
				timer += Time.deltaTime / cameraPath[path].travelTime;
				//Break on timeout
				if (timer > 1.0f)
				{
					moving = false;
					current = cameraPath[path].endPoint;
				}
			}
			//Lerp rotation and position between points
			WATransform[] pos = new WATransform[cameraPath[path].pos.Length + 2];
			//First the start point
			pos[0] = new WATransform();
			pos[0].pos = endPoints[cameraPath[path].startPoint].transform.position;
			pos[0].rot = endPoints[cameraPath[path].startPoint].transform.rotation;
			int i = 0;
			for (; i < cameraPath[path].pos.Length; i++)
			{
				pos[i + 1] = new WATransform();
				pos[i + 1].pos = cameraPath[path].pos[i].position;
				pos[i + 1].rot = cameraPath[path].pos[i].localRotation;
			}
			//Lastly the endpoint
			pos[i + 1] = new WATransform();
			pos[i + 1].pos = endPoints[cameraPath[path].endPoint].transform.position;
			pos[i + 1].rot = endPoints[cameraPath[path].endPoint].transform.rotation;
			//Process
			WATransform output = PositionLerpRecursive(pos, timer);
			mainCamera.transform.position = output.pos;
			mainCamera.transform.localRotation = output.rot;
		}
		else //Deny input commands while move in progress
		{
			switch (state)
			{
				case MENU_STATE.MENU:
					break;
				case MENU_STATE.CHARACTER_SELECT:
					//Player 1 selection placeholder
					if (Input.GetKeyDown(KeyCode.LeftArrow))
					{

					}
					else if (Input.GetKeyDown(KeyCode.RightArrow))
					{

					}
					//Player 2 selection placeholder
					if (Input.GetKeyDown(KeyCode.A))
					{

					}
					if (Input.GetKeyDown(KeyCode.D))
					{

					}
					break;
			}
			//These should be replaced with XBocks controller inputs
			if (Input.GetKeyDown(KeyCode.Return))
			{
				endPoints[current].call.Invoke();
			}
		}
	}

	//Checks if the target has a valid path to it
	public bool MoveTo(int _target)
	{
		for (int i = 0; i < cameraPath.Length; i++)
		{
			//Target is valid to move to in the correct direction
			if (cameraPath[i].startPoint == current &&
				cameraPath[i].endPoint == _target)
			{
				timer = 0.0f;
				reverse = false;
				path = i;
				moving = true;

				return true;
			}
			//Target is valid to move to in the reverse direction
			else if (cameraPath[i].startPoint == _target &&
				cameraPath[i].endPoint == current)
			{
				timer = 1.0f;
				reverse = true;
				path = i;
				moving = true;

				return true;
			}
		}
		//we didnt find a path we could follow so we return false
		return false;
	}

	//Non-returning copy of MoveTo to use with UnityEvent class
	public void V_MoveTo(int _target)
	{
		MoveTo(_target);
	}

	//Move the camera to the end of a path by force, skipping any setup pathing
	public void ForceMoveTo(int _target)
	{
		current = _target;
		mainCamera.transform.position = endPoints[_target].transform.position;
		mainCamera.transform.rotation = endPoints[_target].transform.rotation;
	}

	public void GotoCharacterSelection()
	{
		V_MoveTo(2); //If theres a problem it will be this -----------------------------------------------------------------------------------------------
		state = MENU_STATE.CHARACTER_SELECT;
	}

	public void CharacterSelectComplete()
	{
		SceneManager.LoadScene(gameScene);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	#region Gizmos_n_Debug
	//Gizmos to help visualize it
	private void OnDrawGizmos()
	{
		foreach (Rails r in cameraPath)
		{
			Gizmos.color = r.color;
			Gizmos.DrawLine(endPoints[r.startPoint].transform.position, r.pos[0].position);
			int i = 0;
			for (; i < r.pos.Length - 1; ++i)
			{
				Gizmos.DrawLine(r.pos[i].position, r.pos[i + 1].position);
				Gizmos.DrawRay(r.pos[i].position, r.pos[i].forward);
			}
			Gizmos.DrawLine(endPoints[r.endPoint].transform.position, r.pos[i].position);
		}
		//Draw the endpoints and the directions their facing
		Gizmos.color = Color.white;
		foreach (Points t in endPoints)
		{
			Gizmos.DrawSphere(t.transform.position, 0.2f);
			Gizmos.DrawRay(t.transform.position, t.transform.forward);
		}
	}

	//More indepth
	private void OnDrawGizmosSelected()
	{
		foreach (Rails r in cameraPath)
		{
			Gizmos.color = r.color;
			WATransform[] pos = new WATransform[r.pos.Length + 2];
			pos[0] = new WATransform();
			pos[0].pos = endPoints[r.startPoint].transform.position;
			pos[0].rot = endPoints[r.startPoint].transform.rotation;
			int i = 0;
			for (; i < r.pos.Length; i++)
			{
				pos[i + 1] = new WATransform();
				pos[i + 1].pos = r.pos[i].position;
				pos[i + 1].rot = r.pos[i].localRotation;
			}
			pos[i + 1] = new WATransform();
			pos[i + 1].pos = endPoints[r.endPoint].transform.position;
			pos[i + 1].rot = endPoints[r.endPoint].transform.rotation;
			WATransform pos1 = PositionLerpRecursive(pos, 0.1f);
			Gizmos.DrawLine(pos[0].pos, pos1.pos);
			WATransform pos2 = PositionLerpRecursive(pos, 0.2f);
			Gizmos.DrawLine(pos1.pos, pos2.pos);
			pos1 = PositionLerpRecursive(pos, 0.3f);
			Gizmos.DrawLine(pos2.pos, pos1.pos);
			pos2 = PositionLerpRecursive(pos, 0.4f);
			Gizmos.DrawLine(pos1.pos, pos2.pos);
			pos1 = PositionLerpRecursive(pos, 0.5f);
			Gizmos.DrawLine(pos2.pos, pos1.pos);
			pos2 = PositionLerpRecursive(pos, 0.6f);
			Gizmos.DrawLine(pos1.pos, pos2.pos);
			pos1 = PositionLerpRecursive(pos, 0.7f);
			Gizmos.DrawLine(pos2.pos, pos1.pos);
			pos2 = PositionLerpRecursive(pos, 0.8f);
			Gizmos.DrawLine(pos1.pos, pos2.pos);
			pos1 = PositionLerpRecursive(pos, 0.9f);
			Gizmos.DrawLine(pos2.pos, pos1.pos);
			Gizmos.DrawLine(pos1.pos, pos[pos.Length - 1].pos);
		}
	}
	#endregion

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
