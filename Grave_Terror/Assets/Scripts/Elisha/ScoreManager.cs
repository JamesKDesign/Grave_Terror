 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score = 0;
    public Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>() as Text;
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {

        ChunkScore();
    }

    void ChunkScore()
    {
        text.text = "Chunk: " + score;
    }
}
