using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    public Text text;
    public static int score = 0;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>() as Text;
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {

        text.text = "Kills: " + score;

	}
}
