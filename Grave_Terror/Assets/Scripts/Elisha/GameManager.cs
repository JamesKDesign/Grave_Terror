using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour {

    public XboxControllerManager xboxController1;
    public PlayerHealth gameOver;

    public void RestartGame()
    {
        
      
       SceneManager.LoadScene(1);
       
    }

    public void Quit()
    {
        
         SceneManager.LoadScene(0);
         
    }

    public void Update()
    {
        if (gameOver.DeathScreen.activeInHierarchy == true)
        {
            if (xboxController1.useController == true)
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
    }
}