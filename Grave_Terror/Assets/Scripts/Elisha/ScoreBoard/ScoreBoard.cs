using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class ScoreBoard : MonoBehaviour
{
    public Text Sizzle;
    public Text chunk;
    private int SizzleScore;
    private int SizzleDowns;
    private int SizzleDeath;
    private int ChunkScore;
    private int ChunkDowns;
    private int ChunkDeath;
    public GameObject scoreBoard;
    public XboxControllerManager XboxController;

    // references
    public PlayerHealth sizzlehealth;
    public PlayerHealth chunkhealth;
    public PlayerShooting chunkShooting;
    public Enemy sizzleShooting;

    // Use this for initialization
    void Awake()
    {
        scoreBoard.SetActive(false);
        // sizzle scores
        SizzleScore = 0;
        SizzleDowns = 0;
        SizzleDeath = 0;

        // chunk scores
        ChunkScore = 0;
        ChunkDowns = 0;
        ChunkDeath = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SizzleScoreBoard();
        ChunkScoreBoard();
        UIScoreControls();
    }

    void SizzleScoreBoard()
    {
        Sizzle.text = "Kills: " + SizzleScore;
        Sizzle.text = "Downs: " + SizzleDowns;
        Sizzle.text = "Deaths: " + SizzleDeath;

        if (sizzlehealth.playerState == PlayerHealth.PlayerState.REVIVE)
        {
            SizzleDowns++;
        }

        if(sizzleShooting.state == E_STATE.IGNITED)
        {
            SizzleScore++;
        }

        if(sizzlehealth.playerState == PlayerHealth.PlayerState.DEAD)
        {
            SizzleDeath++;
        }
    }
    
    void ChunkScoreBoard()
    {
        chunk.text = "Kills: " + ChunkScore;
        chunk.text = "Downs: " + ChunkDowns;
        chunk.text = "Deaths: " + ChunkDeath;

        if (chunkhealth.playerState == PlayerHealth.PlayerState.REVIVE)
        {
            ChunkDowns++;
        }

        if (chunkShooting.target.currentHealth == 0)
        {
            ChunkScore++;
        }

        if (chunkhealth.playerState == PlayerHealth.PlayerState.DEAD)
        {
            ChunkDeath++;
        }
    }

    void UIScoreControls()
    {
        if (XboxController.useController == true)
        {
            // bringing up the score board
            if (XCI.GetButtonDown(XboxButton.Back, XboxController.controller))
            {
                scoreBoard.SetActive(true);
            }
            else if (XCI.GetButtonUp(XboxButton.Back, XboxController.controller))
            {
                scoreBoard.SetActive(false);
            }
        }
        else if (!XboxController.useController)
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