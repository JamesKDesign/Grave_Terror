using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
	bool paused = false;

	private void Start()
	{
		gameObject.GetComponent<Canvas>().enabled = false;
	}
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePause();
		}
		if (paused)
		{
		}
		else
		{
		}
	}

	public void ContinueClick()
	{
		TogglePause();
	}

	public void QuitClick()
	{
		Application.Quit();
	}

	private void TogglePause()
	{
		//Toggle pause state
		paused = !paused;
		//Toggle canvas active
		gameObject.GetComponent<Canvas>().enabled = !gameObject.GetComponent<Canvas>().enabled;
		//Using timescale to pause for now
		if (paused)
			Time.timeScale = 0.0f;
		else
			Time.timeScale = 1.0f;
	}
}
