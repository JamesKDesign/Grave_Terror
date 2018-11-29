using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.SceneManagement;
public class UIPauseMenu : MonoBehaviour
{
	bool paused = false;
    public XboxController controller;
    public bool useController = false;

	private void Start()
	{
		gameObject.GetComponent<Canvas>().enabled = false;
	}
	// Update is called once per frame
	void Update ()
	{
        if(!useController)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
        else if (useController == true)
        {
            if (XCI.GetButtonDown(XboxButton.Start))
            {
                TogglePause();
            }

            if(paused == true && (XCI.GetButtonDown(XboxButton.A)))
            {
                QuitClick();
            }
        }
	}

	public void ContinueClick()
	{
		TogglePause();
	}

	public void QuitClick()
	{
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
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
