using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour {

   // [HideInInspector]
    public PlayerHealth gameOver;
    //[HideInInspector]
    public BossKilled gameWin;
    public XboxControllerManager xboxController1;
    public XboxControllerManager xboxController2;

    public void RestartGame()
    {
       SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void Update()
    {
        if (gameOver.DeathScreen.activeInHierarchy == true)
        {
            if (xboxController1.useController == true || xboxController2.useController == true)
            {
                if (XCI.GetButtonDown(XboxButton.A))
                {
                    RestartGame();
                }

                if (XCI.GetButtonDown(XboxButton.B))
                {
                    Quit();
                }
            }
        }
        else if(gameWin.winGameScreen.activeInHierarchy == true)
        {
            if (xboxController1.useController == true || xboxController2.useController == true)
            {
                if (XCI.GetButtonDown(XboxButton.B))
                {
                    Quit();
                }
            }
        }
    }
}