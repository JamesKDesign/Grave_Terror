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
    public int SizzleScore;
    private int SizzleDowns;
    private int ChunkScore;
    private int ChunkDowns;
    private int Placeholder;
    public GameObject scoreBoard;
    // references
    public XboxControllerManager XboxController1;
    public XboxControllerManager XboxController2;
    public PlayerHealth sizzlehealth;
    public PlayerHealth chunkhealth;
    public PlayerShooting chunkShooting;
    public FlameMovement sizzleShooting;
    public Enemy sizzleShot;
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
        if (XboxController1.useController == true && XboxController2.useController == true)
        {
            // bringing up the score board
            if (XCI.GetButton(XboxButton.Back, XboxController1.controller) || XCI.GetButton(XboxButton.Back, XboxController2.controller))
            {
                print("working controller");
                scoreBoard.SetActive(true);
            }
            else if( XCI.GetButtonUp(XboxButton.Back, XboxController1.controller) || XCI.GetButtonUp(XboxButton.Back, XboxController2.controller))
            {
                scoreBoard.SetActive(false);
            }
        }
        SizzleScoreBoard();
        ChunkScoreBoard();
    }

    void SizzleScoreBoard()
    {

        //if (sizzleShooting.enemy != null)
        //{
        //    if (sizzleShooting.enemy.GetComponent<EnemyHealth>().currentHealth <= 0.0f && sizzleShooting.enemy.GetComponent<Enemy>().alive)
        //    {
        //        SizzleScore += 1;
        //        Sizzle.text = SizzleScore.ToString();
        //    }
        //}
        if (sizzlehealth.currentHealth <= 0f && !sizzlehealth.sizzleDowned)
        {
            SizzleDowns += 1;
            sizzlehealth.sizzleDowned = true;
            SizzleD.text = SizzleDowns.ToString();
        }

    }
    void ChunkScoreBoard()
    {
        if (chunkShooting.target != null)
        {
            if (chunkShooting.target.currentHealth <= 0.0f && 
                chunkShooting.target.gameObject.GetComponent<Enemy>().alive && 
                chunkShooting.target.gameObject.GetComponent<Enemy>().state != E_STATE.IGNITED)
            {
                ChunkScore += 1;
                chunk.text = ChunkScore.ToString();
            }
        }

        if (chunkhealth.currentHealth <= 0f && !chunkhealth.chunkDowned)
        {
            ChunkDowns += 1;
            chunkhealth.chunkDowned = true;
            ChunkD.text = ChunkDowns.ToString();
        }
    }
}