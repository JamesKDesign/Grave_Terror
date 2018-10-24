using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class ScoreBoardUI : MonoBehaviour
{

    [SerializeField]
    GameObject scoreBoard;
    public XboxController controller;
    public bool useController = false;

    // Update is called once per frame
    void Update()
    {

        if (useController == true)
        {
            // bringing up the score board
            if (XCI.GetButtonDown(XboxButton.Back, controller))
            {
                scoreBoard.SetActive(true);
            }
            else if (XCI.GetButtonUp(XboxButton.Back, controller))
            {
                scoreBoard.SetActive(false);
            }
        }
        else if (!useController)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                scoreBoard.SetActive(true);
            }
            else if (Input.GetKeyUp(KeyCode.Tab))
            {
                scoreBoard.SetActive(false);
            }
        }
    }
}