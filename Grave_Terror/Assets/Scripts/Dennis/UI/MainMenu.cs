using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rails
{
	public Transform[] pos;
	public float travelTime;
}

[System.Serializable]
public class ValidPath
{
	public int a;
	public int b;
}

public class WATransform
{
	public Vector3 pos;
	public Quaternion rot;
}

public class MainMenu : MonoBehaviour
{
	Camera mainCamera;

	public Rails[] cameraPath;
	public ValidPath[] paths;

	private int current = 0;

	private bool moving = false;
	private float timer = 0.0f;
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
		//If we want to move somewhere
		if (moving)
		{
			timer += Time.deltaTime / cameraPath[target].travelTime;
			//Break on timeout
			if (timer > 1.0f)
			{
				moving = false;
			}
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

	public void MoveTo(int _target)
	{
		bool invalid = false;
		foreach (ValidPath p in paths)
		{
			if (p.a != current)
				invalid = true;
			if (p.b != _target)
				invalid = true;
		}
		if(invalid)
			return;

		moving = true;
		timer = 0.0f;
		target = _target;
		sequence = 0;
	}

	private void OnDrawGizmos()
	{
		foreach (Rails r in cameraPath)
		{
			for (int i = 0; i < r.pos.Length - 1; ++i)
			{
				Gizmos.DrawLine(r.pos[i].position, r.pos[i + 1].position);
			}
		}
	}

	private WATransform PositionLerpRecursive(WATransform[] _pos, float _time)
	{
		if (_pos.Length == 1)
			return _pos[0];

		WATransform[] pos = new WATransform[_pos.Length - 1];

		for (int i = 0; i < pos.Length; ++i)
		{
			WATransform temp = new WATransform();
			temp.pos = Vector3.Lerp(_pos[i].pos, _pos[i + 1].pos, _time);
			temp.rot = Quaternion.Lerp(_pos[i].rot, _pos[i + 1].rot, _time);
			pos[i] = temp;
		}

		return PositionLerpRecursive(pos, _time);
	}
}
