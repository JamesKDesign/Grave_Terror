// Author: Elisha Anagnostakis
// Date Modified: 28/11/18
// Purpose: This script is the game manager that handles the different scenes that are changed based on players input
// Also handles game over and game win screens and checks if the player can press the buttons

using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour {

    // references
    [HideInInspector]
    public PlayerHealth gameOver;
    [HideInInspector]
    public BossKilled gameWin;
    [HideInInspector]
    public XboxControllerManager xboxController1;
    [HideInInspector]
    public XboxControllerManager xboxController2;
    [HideInInspector]
    public UIPauseMenu pause;

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
        // checks if the death screen is set to active 
        if (gameOver.DeathScreen.activeInHierarchy == true)
        {
            // if it is then the player can press specifed buttons to either restart or quit
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
        // checks if the game win screen is set to active
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