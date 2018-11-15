using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivePlayer : MonoBehaviour {

    public float timer;
    PlayerHealth player;
    public GameObject player2;

    private void Awake()
    {
        player = GetComponentInParent<PlayerHealth>();
        timer = 5.0f;
    }

    private void Update()
    {
        if (timer <= 0f)
        {
            player.playerState = PlayerHealth.PlayerState.ALIVE;
            ParticleSystem.MainModule revMainMod = player.reviveVolume.GetComponentInChildren<ParticleSystem>().main;
            revMainMod.startColor = Color.green;
            player.reviveVolume.GetComponentInChildren<ParticleSystem>().Stop();
            player.health = 3.0f;
            player.currentHealth = player.health;
            timer = 5f;
        
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