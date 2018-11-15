using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivePlayer : MonoBehaviour {

    public float timer;
    PlayerHealth player;
    public GameObject player2;
    public float reviveHealth;
    public float deleteTimer;
    private bool isRevived = false;

    private void Awake()
    {
        player = GetComponentInParent<PlayerHealth>();
        timer = 5.0f;
    }

    private void Update()
    {
        if (timer <= 0f)
        {
            isRevived = true;
            player.playerState = PlayerHealth.PlayerState.ALIVE;
            ParticleSystem.MainModule revMainMod = player.reviveVolume.GetComponentInChildren<ParticleSystem>().main;
            revMainMod.startColor = Color.green;
            player.reviveVolume.GetComponentInChildren<ParticleSystem>().Play();
            player.health = reviveHealth;
            player.currentHealth = player.health;
            timer = 5f;
        }

        if(isRevived == true)
        {
            deleteTimer -= Time.deltaTime;
            // deleting the revive effect after revive
            if (deleteTimer <= 0)
            {
                player.reviveVolume.GetComponentInChildren<ParticleSystem>().Stop();
                deleteTimer = 0.0f;
            }
        }
    }

    // If player enters this trigger it will revive the player
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Activated");
        if (player.playerState == PlayerHealth.PlayerState.REVIVE)
        {
            if (other.gameObject == player2)
            {
                timer -= Time.deltaTime;
                Debug.Log("timer" + timer);
            }
        }
    }
}