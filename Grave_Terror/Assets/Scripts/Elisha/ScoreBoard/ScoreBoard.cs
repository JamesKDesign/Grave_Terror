using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{

    public Text killsText;
    public static int score = 0;

    // Use this for initialization
    void Start()
    {
        killsText = GetComponent<Text>() as Text;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {

        killsText.text = "Kills: " + score;
    }
}
