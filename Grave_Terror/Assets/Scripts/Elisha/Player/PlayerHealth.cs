﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Tooltip("Players health amount")]
    public float health;
    [Tooltip("The players current health state")]
    public float currentHealth;
    [Tooltip("How long the flash will run for when the player is hit")]
    public float timer;
    PlayerMovement controls;
    public GameObject reviveVolume;
    //public GameObject DeathScreen;
    public Animator anim;
    public Tether Tet;
    public Transform player2;


    public enum PlayerState
    {
        ALIVE,
        REVIVE,
        DEAD
    }

    public PlayerState playerState;


    private void Awake()
    {
        //DeathScreen.SetActive(false);
        controls = GetComponent<PlayerMovement>();
        currentHealth = health;
    }

    public void Update()
    {
        playerHealth();

        // Player state switch
        switch(playerState)
        {
            // Player alive with full functions
            case PlayerState.ALIVE:
                print("In alive state");

                anim.SetBool("IsDowned", false);
                timer = 20f;
                controls.Move();
               // controls.Dashing();
                controls.Turning();
                break;
            // Player revive state can rotate player and shoot 
            case PlayerState.REVIVE:
                print("In revive state");

                anim.SetBool("IsDowned", true);
                // death timer hits 0 will kill the player 
                if (timer == 0.0f)
                {
                    playerState = PlayerState.DEAD;
                }
                break;
            // Player dead state sets player to in-active
            case PlayerState.DEAD:
                print("In death state");
                break;
        }

        if(player2.GetComponent<PlayerHealth>().playerState == PlayerState.DEAD && playerState == PlayerState.DEAD)
        {
            //DeathScreen.SetActive(true);
            player2.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        if (playerState == PlayerState.DEAD) {
            Tet.maxDistance = float.PositiveInfinity;
            this.gameObject.SetActive(false);
        }

        if (playerState == PlayerState.REVIVE)
        {
            timer -= Time.deltaTime;
            if (reviveVolume != null)
            {
                ParticleSystem.MainModule revMainMod = reviveVolume.GetComponentInChildren<ParticleSystem>().main;
                revMainMod.startColor = Color.red;
                reviveVolume.GetComponentInChildren<ParticleSystem>().Play();
            }
        }
    }

    public void DamagePlayer(float amount)
    {
        currentHealth -= amount;
        print("Player health: " + currentHealth);
    }

    void playerHealth ()
    {
        if (currentHealth <= 0)
        {
            playerState = PlayerState.REVIVE;         
        }
        else
        {
            playerState = PlayerState.ALIVE;
        }
    }
}