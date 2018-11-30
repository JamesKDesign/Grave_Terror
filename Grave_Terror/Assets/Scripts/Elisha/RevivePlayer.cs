// Author: Elisha Anagnostakis
// Date Modified: 20/11/18
// Purpose: This script allows the player to revive there team mates when in a revive state

using UnityEngine;

public class RevivePlayer : MonoBehaviour {

    // time at which the revive takes to heal the player
    public float Revivetimer;
    // reference to the players health
    PlayerHealth player;
    // reference to the second player
    public GameObject player2;
    // how much health is being set once revived
    public float reviveHealth;
    // time at which it takes for the particle system to disappear
    public float deleteTimer;
    // simple bool checking if the player is revived or not
    private bool isRevived = false;

    private void Awake()
    {
        player = GetComponentInParent<PlayerHealth>();
        Revivetimer = 5.0f;
    }

    private void Update()
    {
        // if the revie timer is finished
        if (Revivetimer <= 0f)
        {
            // player state gets set to alive
            isRevived = true;
            player.playerState = PlayerHealth.PlayerState.ALIVE;
            // particle system goes green 
            ParticleSystem.MainModule revMainMod = player.reviveVolume.GetComponentInChildren<ParticleSystem>().main;
            revMainMod.startColor = Color.green;
            // plays the particle
            player.reviveVolume.GetComponentInChildren<ParticleSystem>().Play();
            // heals the player thats down
            player.health = reviveHealth;
            player.currentHealth = player.health;
            // timer gets set back
            Revivetimer = 5f;
        }
        // if the player is revived 
        if(isRevived == true)
        {
            // have the timer go down 
            deleteTimer -= Time.deltaTime;
            // deleting the revive effect after revive
            if (deleteTimer <= 0)
            {
                player.reviveVolume.GetComponentInChildren<ParticleSystem>().Stop();
                deleteTimer = 2.0f;
                isRevived = false;
            }
        }
    }

    // If player enters this trigger it will revive the player
    void OnTriggerStay(Collider other)
    {
        // if player is in revive state
        if (player.playerState == PlayerHealth.PlayerState.REVIVE)
        {
            // and the player 2 enters the revive radius
            if (other.gameObject == player2 && player.isReviving == false)
            {
                // start reviving
                player.isReviving = true;

                if(player.isReviving == true)
                {
                    // revive time depletes
                    Revivetimer -= Time.deltaTime;
                    // if player is in revive radius have the particle change to green 
                    ParticleSystem.MainModule revMainMod = player.reviveVolume.GetComponentInChildren<ParticleSystem>().main;
                    revMainMod.startColor = Color.green;
                    player.reviveVolume.GetComponentInChildren<ParticleSystem>().Play();
                }
            }
            else
            {
                player.isReviving = false;
            }
        }
    }
}