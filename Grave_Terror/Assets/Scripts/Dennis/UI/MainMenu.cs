using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XboxCtrlrInput;

public enum MENU_STATE
{
	MENU,
	//OPTIONS,
	QUIT,
	CHARACTER_SELECT,
	GAME,
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
	[Tooltip("Element no. of desired destination when navigating menu\n-1 will do nothing")]
	public int movePositive;
	[Tooltip("Element no. of desired destination when navigating menu\n-1 will do nothing")]
	public int moveNegative;
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
	private string menuScene;

	//The Canvas
	public Canvas canvas;

	//Both players
	public XboxControllerManager xboxController;
	//Now appart
	public XboxControllerManager p1XCtrl;
	public XboxControllerManager p2XCtrl;

	[Header("Camera")]
	public Points[] endPoints;
	[Tooltip("Transforms that the camera will travel between\nAffected by rotation!")]
	public Rails[] cameraPath;

	public MENU_STATE state = MENU_STATE.MENU;

	//We start at nothing and follow the entrance path
	private int current = -1;

	//Is the camera in motion?
	private bool moving = false; //Variable is reused for gameover
	//Timer for the camera in motion
	private float timer = 0.0f;
	//The target path we are taking
	private int path = 0;
	//If we are following it in reverse order
	private bool reverse = false;

	//Selection -1 is invalid, Selection 1 is Chunk, Selection 2 is Sizzle
	private int p1Selected = -1;
	private int p2Selected = -1;

	public GameObject selectionScreen;
	public float selectionScreenMovespeed = 300.0f;

	//Selection Screen cursor
	Vector2 p1Vec, p2Vec;
	GameObject oChu, oSiz;
	Rect rChu, rSiz;
	GameObject p1Cursor, p2Cursor;

	//Game in progress sequence
	[Header("Game Settings")]
	private PlayerHealth[] player = null;
	private GameObject ogreScreen = null;
	[Tooltip("Time until fully")]
	public float fadeTime;

	// Use this for initialization
	void Start()
	{
		//SceneManager stuff
		SceneManager.sceneLoaded += OnSceneLoad;
		menuScene = SceneManager.GetActiveScene().name;
		//Make sure this persists through scene loads
		DontDestroyOnLoad(gameObject);

		mainCamera = Camera.main;

		ForceMoveTo(0);

		p1Vec = Vector2.zero;
		p2Vec = Vector2.zero;

		foreach (Transform t in selectionScreen.transform)
		{
			if (t.gameObject.name == "ChunkSelection")
			{
				rChu = t.gameObject.GetComponent<RectTransform>().rect;
				oChu = t.gameObject;
			}
			else if (t.gameObject.name == "SizzleSelection")
			{
				rSiz = t.gameObject.GetComponent<RectTransform>().rect;
				oSiz = t.gameObject;
			}
			else if (t.gameObject.name == "P1Cursor")
			{
				p1Cursor = t.gameObject;
			}
			else if (t.gameObject.name == "P2Cursor")
			{
				p2Cursor = t.gameObject;
			}
		}
	}

	//Dont break it...
	private void Just()
	{
		//P1
		p1Vec.x = XCI.GetAxis(XboxAxis.LeftStickX, p1XCtrl.controller);
		p1Vec.y = XCI.GetAxis(XboxAxis.LeftStickY, p1XCtrl.controller);
		//P2
		p2Vec.x = XCI.GetAxis(XboxAxis.LeftStickX, p2XCtrl.controller);
		p2Vec.y = XCI.GetAxis(XboxAxis.LeftStickY, p2XCtrl.controller);
	}

	// Update is called once per frame
	private void MainMenuUpdate()
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
			if (state == MENU_STATE.CHARACTER_SELECT)
			{
				Vector2 p1Selection;
				Vector2 p2Selection;
				//First come first serve selection
				//add some way for players to see who they selected
				p1Selection.x = XCI.GetAxis(XboxAxis.LeftStickX, p1XCtrl.controller);
				p1Selection.y = XCI.GetAxis(XboxAxis.LeftStickY, p1XCtrl.controller);

				p2Selection.x = XCI.GetAxis(XboxAxis.LeftStickX, p2XCtrl.controller);
				p2Selection.y = XCI.GetAxis(XboxAxis.LeftStickX, p2XCtrl.controller);
				//DEBUG just start
				if (Input.GetKeyDown(KeyCode.Z))
				{
					p1Selected = 1;
					p2Selected = 2;
					endPoints[current].call.Invoke();
				}
				//moving at 300 meaningless units a second
				p1Vec += p1Selection * selectionScreenMovespeed * Time.deltaTime;
				p2Vec += p2Selection * selectionScreenMovespeed * Time.deltaTime;

				p1Cursor.GetComponent<RectTransform>().localPosition = p1Vec;
				p2Cursor.GetComponent<RectTransform>().localPosition = p2Vec;
				//Cursor no escape
				Rect edges = selectionScreen.GetComponent<RectTransform>().rect;
				if (p1Vec.x >= edges.xMax)
					p1Vec.x = edges.xMax;
				else if (p1Vec.x <= edges.xMin)
					p1Vec.x = edges.xMin;
				if (p1Vec.y >= edges.yMax)
					p1Vec.y = edges.yMax;
				else if (p1Vec.y <= edges.yMin)
					p1Vec.y = edges.yMin;
				//Cursor no escape 2
				if (p2Vec.x >= edges.xMax)
					p2Vec.x = edges.xMax;
				else if (p2Vec.x <= edges.xMin)
					p2Vec.x = edges.xMin;
				if (p2Vec.y >= edges.yMax)
					p2Vec.y = edges.yMax;
				else if (p2Vec.y <= edges.yMin)
					p2Vec.y = edges.yMin;

				//P1 Poosh
				if (XCI.GetButtonDown(XboxButton.A, p1XCtrl.controller))
				{
					//P1 cursor hitboxes
					if (p1Vec.x <= rChu.xMax && p1Vec.x >= rChu.xMin &&
					p1Vec.y <= rChu.yMax && p1Vec.y >= rChu.yMin)
					{
						if (p2Selected != 1)
						{
							p1Selected = 1;
							oChu.GetComponent<CanvasRenderer>().SetColor(Color.red);
						}
						else
						{
							//p1Selected = -1;
						}
					}
					else if (p1Vec.x <= rSiz.xMax && p1Vec.x >= rSiz.xMin &&
						p1Vec.y <= rSiz.yMax && p1Vec.y >= rSiz.yMin)
					{
						if (p2Selected != 2)
						{
							p1Selected = 2;
							oSiz.GetComponent<CanvasRenderer>().SetColor(Color.red);
						}
						else
						{
							//p1Selected = -1;
						}
					}
					else
					{
						p1Selected = -1;
					}
				}
				else if (XCI.GetButtonDown(XboxButton.B, p1XCtrl.controller))
				{
					//Reset color to white
					if(p1Selected == 1)
						oChu.GetComponent<CanvasRenderer>().SetColor(Color.white);
					else if(p1Selected == 2)
						oSiz.GetComponent<CanvasRenderer>().SetColor(Color.white);
					//Clear selected
					p1Selected = -1;
				}
				//P2 Poosh
				if (XCI.GetButtonDown(XboxButton.A, p2XCtrl.controller))
				{
					//P2 cursor hitboxes
					if (p2Vec.x <= rChu.xMax && p2Vec.x >= rChu.xMin &&
						p2Vec.y <= rChu.yMax && p2Vec.y >= rChu.yMin)
					{
						if (p1Selected != 1)
						{
							p2Selected = 1;
							oChu.GetComponent<CanvasRenderer>().SetColor(Color.blue);
						}
						else
						{
							//p2Selected = -1;
						}
					}
					else if (p2Vec.x <= rSiz.xMax && p2Vec.x >= rSiz.xMin &&
						p2Vec.y <= rSiz.yMax && p2Vec.y >= rSiz.yMin)
					{
						if (p1Selected != 2)
						{
							p2Selected = 2;
							oSiz.GetComponent<CanvasRenderer>().SetColor(Color.blue);
						}
						else
						{
							//p2Selected = -1;
						}
					}
					else
					{
						p2Selected = -1;
					}
				}
				else if (XCI.GetButtonDown(XboxButton.B, p2XCtrl.controller))
				{
					//Reset color to white
					if (p2Selected == 1)
						oChu.GetComponent<CanvasRenderer>().SetColor(Color.white);
					else if (p2Selected == 2)
						oSiz.GetComponent<CanvasRenderer>().SetColor(Color.white);
					//Clear selected
					p2Selected = -1;
				}
			}
			//Debug keyboard buttons
			if (Input.GetKeyDown(KeyCode.Return))
			{
				endPoints[current].call.Invoke();
			}
			//Positive Movement
			else if (Input.GetKeyDown(KeyCode.D))
			{
				if (endPoints[current].movePositive != -1)
				{
					MoveTo(endPoints[current].movePositive);
				}
			}
			//Negative movement
			else if (Input.GetKeyDown(KeyCode.A))
			{
				if (endPoints[current].moveNegative != -1)
				{
					MoveTo(endPoints[current].moveNegative);
				}
			}

			//Xbone controller inputs
			if (xboxController.useController)
			{
				float horiAxis = XCI.GetAxis(XboxAxis.LeftStickX);
				//Confirm
				if (XCI.GetButtonDown(XboxButton.A, xboxController.controller))
				{
					endPoints[current].call.Invoke();
				}
				else if (horiAxis > 0.5f) //Positive Movement
				{
					MoveTo(endPoints[current].movePositive);
				}
				else if (horiAxis < -0.5f) //Negative Movement
				{
					MoveTo(endPoints[current].moveNegative);
				}
			}
		}
	}

	private void GameUpdate()
	{
		if (moving) //If both players are dead
		{
			if (timer <= 0.0f)
			{
				//SceneManager.LoadScene(menuScene);
				ogreScreen.GetComponent<Image>().color = Color.white;

				//Accept player input here
				if (Input.GetKeyDown(KeyCode.Z))
				{
					SceneManager.LoadScene(gameScene);
				}
				else if (Input.GetKeyDown(KeyCode.X))
				{
					SceneManager.LoadScene(menuScene);
				}
				//Xbone Controller
				if (xboxController.useController)
				{
					float horiAxis = XCI.GetAxis(XboxAxis.LeftStickX);
					//PUSH THE BUTTON
					if (XCI.GetButtonDown(XboxButton.A, xboxController.controller))
					{
						if (horiAxis > 0.5f) //Retry
						{
							SceneManager.LoadScene(gameScene);
						}
						else if (horiAxis < -0.5f) //Main Menu
						{
							SceneManager.LoadScene(menuScene);
						}
					}
				}
			}
			else
			{
				timer -= Time.deltaTime / fadeTime;
				//no write variables
				Color color = ogreScreen.GetComponent<Image>().color;
				color.a = (timer * -2.0f) + 1.0f;
				ogreScreen.GetComponent<Image>().color = color;
			}
		}
		//Check if both players are dead
		else if (player[0].playerState == PlayerHealth.PlayerState.DEAD &&
				player[0].playerState == PlayerHealth.PlayerState.DEAD)
		{
			//reusing old variables
			moving = true;
			timer = 1.0f;
			ogreScreen.SetActive(true);
			ogreScreen.GetComponentInChildren<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		}
	}

	private void OnSceneLoad(Scene _scene, LoadSceneMode _mode)
	{
		if (_scene.name == gameScene)
		{
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			player = new PlayerHealth[2];
			player[0] = players[0].GetComponent<PlayerHealth>();
			player[1] = players[1].GetComponent<PlayerHealth>();

			ogreScreen = GameObject.Find("GameOverScreen");
			ogreScreen.SetActive(false);

			timer = 0.0f;
			moving = false;

			//Override player controller settings
			if (p1Selected == 0)
			{
				players[0].GetComponent<PlayerMovement>().xboxController.controller = XboxController.First;
				players[1].GetComponent<PlayerMovement>().xboxController.controller = XboxController.Second;
			}
			else
			{
				players[1].GetComponent<PlayerMovement>().xboxController.controller = XboxController.Second;
				players[0].GetComponent<PlayerMovement>().xboxController.controller = XboxController.First;
			}
		}
		else if (_scene.name == menuScene)
		{
			p1Vec = Vector2.zero;
			p2Vec = Vector2.zero;
		}
	}

	private void Update()
	{
		switch (state)
		{
			case MENU_STATE.CHARACTER_SELECT:
			case MENU_STATE.MENU:
				MainMenuUpdate();
				break;
			case MENU_STATE.GAME:
				GameUpdate();
				break;
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
		state = MENU_STATE.CHARACTER_SELECT;
		selectionScreen.SetActive(true);
	}

	public void CharacterSelectComplete()
	{
		//Make sure both players have made a selection before allowing them to proceed
		if (p1Selected != -1 &&
			p2Selected != -1)
		{
			SceneManager.LoadScene(gameScene);
			state = MENU_STATE.GAME;
		}
	}

	public void QuitGame()
	{
		//Set state to quit just incase
		state = MENU_STATE.QUIT;
		Debug.Log("Quitting..."); //Debug message to show that it works in editor
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
			//Segment 2
			{
				Gizmos.color = r.color;
				WATransform[] pos = new WATransform[r.pos.Length + 2];
				pos[0] = new WATransform();
				pos[0].pos = endPoints[r.startPoint].transform.position;
				pos[0].rot = endPoints[r.startPoint].transform.rotation;
				i = 0;
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
		//Draw the endpoints and the directions their facing
		Gizmos.color = Color.white;
		foreach (Points t in endPoints)
		{
			Gizmos.DrawSphere(t.transform.position, 0.2f);
			Gizmos.DrawRay(t.transform.position, t.transform.forward);
		}
	}

	//More indepth
	//private void OnDrawGizmosSelected()
	//{
	//	foreach (Rails r in cameraPath)
	//	{
	//		Gizmos.color = r.color;
	//		WATransform[] pos = new WATransform[r.pos.Length + 2];
	//		pos[0] = new WATransform();
	//		pos[0].pos = endPoints[r.startPoint].transform.position;
	//		pos[0].rot = endPoints[r.startPoint].transform.rotation;
	//		int i = 0;
	//		for (; i < r.pos.Length; i++)
	//		{
	//			pos[i + 1] = new WATransform();
	//			pos[i + 1].pos = r.pos[i].position;
	//			pos[i + 1].rot = r.pos[i].localRotation;
	//		}
	//		pos[i + 1] = new WATransform();
	//		pos[i + 1].pos = endPoints[r.endPoint].transform.position;
	//		pos[i + 1].rot = endPoints[r.endPoint].transform.rotation;
	//		WATransform pos1 = PositionLerpRecursive(pos, 0.1f);
	//		Gizmos.DrawLine(pos[0].pos, pos1.pos);
	//		WATransform pos2 = PositionLerpRecursive(pos, 0.2f);
	//		Gizmos.DrawLine(pos1.pos, pos2.pos);
	//		pos1 = PositionLerpRecursive(pos, 0.3f);
	//		Gizmos.DrawLine(pos2.pos, pos1.pos);
	//		pos2 = PositionLerpRecursive(pos, 0.4f);
	//		Gizmos.DrawLine(pos1.pos, pos2.pos);
	//		pos1 = PositionLerpRecursive(pos, 0.5f);
	//		Gizmos.DrawLine(pos2.pos, pos1.pos);
	//		pos2 = PositionLerpRecursive(pos, 0.6f);
	//		Gizmos.DrawLine(pos1.pos, pos2.pos);
	//		pos1 = PositionLerpRecursive(pos, 0.7f);
	//		Gizmos.DrawLine(pos2.pos, pos1.pos);
	//		pos2 = PositionLerpRecursive(pos, 0.8f);
	//		Gizmos.DrawLine(pos1.pos, pos2.pos);
	//		pos1 = PositionLerpRecursive(pos, 0.9f);
	//		Gizmos.DrawLine(pos2.pos, pos1.pos);
	//		Gizmos.DrawLine(pos1.pos, pos[pos.Length - 1].pos);
	//	}
	//}
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
