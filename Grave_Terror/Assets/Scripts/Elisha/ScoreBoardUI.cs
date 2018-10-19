 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class ScoreBoardUI: MonoBehaviour {

    [SerializeField]
    GameObject scoreBoard;
    public XboxController controller;
	
	// Update is called once per frame
	void Update () {

        // bringing up the score board
        if(XCI.GetButtonDown(XboxButton.Back, controller))
        {
            scoreBoard.SetActive(true);
        }
        else if(XCI.GetButtonUp(XboxButton.Back, controller))
        {
            scoreBoard.SetActive(false);
        }
    }
}