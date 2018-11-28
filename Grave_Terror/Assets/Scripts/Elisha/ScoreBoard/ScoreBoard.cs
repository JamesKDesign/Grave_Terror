using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;
    //ATREUS
public class ScoreBoard : MonoBehaviour
{
    public Text Sizzle;
    public Text chunk;
    public Text SizzleD;
    public Text ChunkD;
    private int SizzleScore;
    private int SizzleDowns;
    private int ChunkScore;
    private int ChunkDowns;
    private int Placeholder;
    public GameObject scoreBoard;
    public XboxControllerManager XboxController1;
    public XboxControllerManager XboxController2;
    // references
    public PlayerHealth sizzlehealth;
    public PlayerHealth chunkhealth;
    public PlayerShooting chunkShooting;
    public EnemyHealth sizzleShooting;
    private bool CheckMe = false;


    // Use this for initialization
    void Awake()
    {
        scoreBoard.SetActive(false);
        // sizzle scores
        SizzleScore = 0;
        SizzleDowns = 0;
        // chunk scores
        ChunkScore = 0;
        ChunkDowns = 0;
    }

    public void Update()
    {
        ChunkScoreBoard();
        SizzleScoreBoard();
        if (XboxController1.useController == true && XboxController2.useController == true)
        {
            // bringing up the score board
            if (XCI.GetButtonDown(XboxButton.Back, XboxController1.controller) || XCI.GetButtonDown(XboxButton.Back, XboxController2.controller))
            {
                print("working controller");
                scoreBoard.SetActive(true);
            }
            else if( XCI.GetButtonUp(XboxButton.Back, XboxController1.controller) || XCI.GetButtonUp(XboxButton.Back, XboxController2.controller))
            {
                scoreBoard.SetActive(false);
            }
        }
    }

    void SizzleScoreBoard()
    {
        if (sizzleShooting.currentHealth <= 0)
        {
            SizzleScore += 1;
            Sizzle.text = SizzleScore.ToString();
        }

        if (sizzlehealth.currentHealth <= 0)
        {
            SizzleDowns += 1;
            Sizzle.text = SizzleDowns.ToString();
        }

    }
    void ChunkScoreBoard()
    {
        Debug.Log(chunkShooting.target.currentHealth);
        if (chunkShooting.target.currentHealth <= 0.0f)
        {
            ChunkScore += 1;
            chunk.text = ChunkScore.ToString();
        }

        if (chunkhealth.currentHealth <= 0)
        {
            ChunkDowns += 1;
            chunk.text = ChunkDowns.ToString();
        }
    }
}