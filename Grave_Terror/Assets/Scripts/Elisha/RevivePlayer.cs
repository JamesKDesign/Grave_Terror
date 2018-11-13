using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivePlayer : MonoBehaviour {

    public float timer;
    PlayerHealth player;
    public Transform player2;


    private void Awake()
    {
        player2 = GetComponent<Transform>();
        player = GetComponent<PlayerHealth>();
    }

    // If player enters this trigger it will revive the player
    public void OnTriggerStay(Collider other)
    {
        if (player.playerState == PlayerHealth.PlayerState.REVIVE)
        {
            if (other.gameObject == player2)
            {
                timer -= Time.deltaTime;
                Debug.Log("timer" + timer);

                if (timer <= 0f)
                {
                    player.playerState = PlayerHealth.PlayerState.ALIVE;
                    timer = 0f;
                }
            }
        }
    }
}