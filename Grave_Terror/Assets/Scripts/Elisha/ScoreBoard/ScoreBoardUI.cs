using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class ScoreBoardUI : MonoBehaviour
{
    [SerializeField]
    GameObject scoreBoard;
    public GameObject player1;
    public GameObject player2;
    public XboxControllerManager xboxController;

    // Update is called once per frame
    void Update()
    {
        if (xboxController.useController == true)
        {
            // bringing up the score board
            if (XCI.GetButtonDown(XboxButton.Back, xboxController.controller))
            {
                scoreBoard.SetActive(true);
            }
            else if (XCI.GetButtonUp(XboxButton.Back, xboxController.controller))
            {
                scoreBoard.SetActive(false);
            }
        }
        else if (!xboxController.useController)
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