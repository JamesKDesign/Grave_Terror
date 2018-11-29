// Author: Elisha Anagnostakis
// Date Modified: 29/11/18
// Purpose: This script handles the state of the game score board that reads the players kills and downs count throughout the entire game

using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;
    //ATREUS
public class ScoreBoard : MonoBehaviour
{
    // text references to both players kills and downs strings 
    public Text Sizzle;
    public Text chunk;
    public Text SizzleD;
    public Text ChunkD;

    // players downs and kills 
	[HideInInspector]
    public int SizzleScore;
    private int SizzleDowns;
    private int ChunkScore;
    private int ChunkDowns;

    // the main scoreboard object on the canvas
    public GameObject scoreBoard;

    // references
    public XboxControllerManager XboxController1;
    public XboxControllerManager XboxController2;
    public PlayerHealth sizzlehealth;
    public PlayerHealth chunkhealth;
    public PlayerShooting chunkShooting;
    public FlameMovement sizzleShooting;
    public UIPauseMenu pause;
    public GameManager manager;
    

    // Use this for initialization
    void Awake()
    {
        // sets the score board to false during the awake of the game
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
        // checks if both players controllers are true 
        if (XboxController1.useController == true || XboxController2.useController == true)
        {
            // if the pause, game win and game over screens are not on the screen THEN
            if(pause.paused == false && manager.gameWin.winGameScreen.activeInHierarchy == false && manager.gameOver.DeathScreen.activeInHierarchy == false)
            {
                // the players can turn on the scoreboard screen
                // bringing up the score board
                if (XCI.GetButton(XboxButton.Back, XboxController1.controller) || XCI.GetButton(XboxButton.Back, XboxController2.controller))
                {
                    scoreBoard.SetActive(true);
                }
                // if the button is released the scoreboard will disappear from the screen
                else if (XCI.GetButtonUp(XboxButton.Back, XboxController1.controller) || XCI.GetButtonUp(XboxButton.Back, XboxController2.controller))
                {
                    scoreBoard.SetActive(false);
                }
            }
        }
        // updates both scores
        SizzleScoreBoard();
        ChunkScoreBoard();
    }

    // sizzles kill and death scores
    void SizzleScoreBoard()
    {
        // checks if sizzles health is 0 and sizzle is not down
        if (sizzlehealth.currentHealth <= 0f && !sizzlehealth.sizzleDowned)
        {
            // add 1 to sizzles down score
            SizzleDowns += 1;
            // then switch the downed booleon to true so it only adds 1 to the score
            sizzlehealth.sizzleDowned = true;
            // updates the 1 to the score board
            SizzleD.text = SizzleDowns.ToString();
        }

    }

    // chunks kill and death scores
    void ChunkScoreBoard()
    {
        // checks if the enemy target is not null
        if (chunkShooting.target != null)
        {
            // then checks if the target is dead from the raycast script
            if (chunkShooting.target.currentHealth <= 0.0f && 
                chunkShooting.target.gameObject.GetComponent<Enemy>().alive && 
                chunkShooting.target.gameObject.GetComponent<Enemy>().state != E_STATE.IGNITED)
            {
                // adds 1 to chunks kills
                ChunkScore += 1;
                chunk.text = ChunkScore.ToString();
            }
        }

        // checks if chunk is dead and down 
        if (chunkhealth.currentHealth <= 0f && !chunkhealth.chunkDowned)
        {
            // adds 1 to chunks down score
            ChunkDowns += 1;
            chunkhealth.chunkDowned = true;
            ChunkD.text = ChunkDowns.ToString();
        }
    }
}