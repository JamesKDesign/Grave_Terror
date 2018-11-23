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
    public XboxControllerManager XboxController;
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

    // Update is called once per frame
    void FixedUpdate()
    {
        SizzleScoreBoard();
        ChunkScoreBoard();     
    }

    private void Update()
    {
        if (XboxController.useController == true)
        {
            // bringing up the score board
            if (XCI.GetButtonDown(XboxButton.Back, XboxController.controller))
            {
                print("working controller");
                scoreBoard.SetActive(true);
            }
            else if (XCI.GetButtonUp(XboxButton.Back, XboxController.controller))
            {
                scoreBoard.SetActive(false);
            }
        }
    }

    void SizzleScoreBoard()
    {
        if (sizzlehealth.playerState == PlayerHealth.PlayerState.REVIVE && CheckMe == true)
        {
            CheckMe = false;
            SizzleDowns += 1;
            SizzleD.text = SizzleDowns.ToString();
        }
        else if (sizzlehealth.playerState != PlayerHealth.PlayerState.REVIVE && CheckMe == false)
        {
            CheckMe = true;
        }

        if (sizzleShooting.currentHealth <= 0 && CheckMe == true)
        {
            CheckMe = false;
            SizzleScore += 1;
            Sizzle.text = SizzleScore.ToString();
        }
        else if (sizzleShooting.currentHealth != 0f && CheckMe == false)
        {
            CheckMe = true;
        }

    }
    void ChunkScoreBoard()
    {

        if (chunkhealth.playerState == PlayerHealth.PlayerState.REVIVE && CheckMe == true)
        {
            CheckMe = false;
            ChunkDowns += 1;
            ChunkD.text = ChunkDowns.ToString();
        }
        else if (chunkhealth.playerState != PlayerHealth.PlayerState.REVIVE && CheckMe == false)
        {
            CheckMe = true;
        }

        if (chunkShooting.target.health <= 0f && CheckMe == true)
        {
            CheckMe = false;
            ChunkScore += 1;
            chunk.text = ChunkScore.ToString();
        }
        else if (chunkShooting.target.health != 0f && CheckMe == false)
        {
            CheckMe = true;
        }
    }
}