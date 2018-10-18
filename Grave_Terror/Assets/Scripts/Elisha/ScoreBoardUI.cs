 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardUI: MonoBehaviour {

    [SerializeField]
    GameObject scoreBoard;
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            scoreBoard.SetActive(true);
        }
        else if(Input.GetKeyUp(KeyCode.Tab))
        {
            scoreBoard.SetActive(false);
        }
    }
}