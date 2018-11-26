using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Tooltip("Players health amount")]
    public float health;
    [Tooltip("The players current health state")]
    public float currentHealth;
    [Tooltip("How long the flash will run for when the player is hit")]
    public float DeathTimer;
    PlayerMovement controls;
    public GameObject reviveVolume;
    public GameObject DeathScreen;
    public Animator anim;
    public Tether Tet;
    public Transform player2;

    private AudioSource audioSource;
    public AudioClip chunkDownClip;
    public AudioClip chunkReviveClip;
    private bool chunkDowned = false;

    // player states
    public enum PlayerState
    {
        ALIVE,
        REVIVE,
        DEAD
    }

    public PlayerState playerState;

    private void Awake()
    {
        currentHealth = health;
        DeathScreen.SetActive(false);
        controls = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        playerHealth();

        // Player state switch statement
        switch(playerState)
        {
            // Player alive state with all functions active
            case PlayerState.ALIVE:
                print("In alive state");
                anim.SetBool("IsDowned", false);
                DeathTimer = 20f;
                controls.Move();
               // controls.Dashing();
                controls.Turning();
                break;

            // Player revive state can rotate player and shoot 
            case PlayerState.REVIVE:
                print("In revive state");
                anim.SetBool("IsDowned", true);

                //TODO
                //1audioSource.PlayOneShot(chunkDownClip);

                // if the timer is 0 call dead state to kill the player permanently
                if (DeathTimer <= 0.0f)
                    playerState = PlayerState.DEAD;
                break;

            // Player dead state sets player to in-active
            case PlayerState.DEAD:

                //TODO
                //audioSource.PlayOneShot(chunkReviveClip);
                print("In death state");
                break;
        }

        // checks if both players are in dead state to then spawn game over screen
        if (player2.GetComponent<PlayerHealth>().playerState == PlayerState.DEAD && playerState == PlayerState.DEAD)
        {
            DeathScreen.SetActive(true);
            player2.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        // checks if both players are in revive state to spawn game over screen
        if(player2.GetComponent<PlayerHealth>().playerState == PlayerState.REVIVE && playerState == PlayerState.REVIVE)
        {
            DeathScreen.SetActive(true);
            player2.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        // checks if one player is in dead state and if the other is in revive state to then spawn game over screen
        else if(player2.GetComponent<PlayerHealth>().playerState == PlayerState.DEAD && playerState == PlayerState.REVIVE)
        {
            DeathScreen.SetActive(true);
            gameObject.SetActive(false);
        }

        // if one player is dead then deactivate tether restriction
        if (playerState == PlayerState.DEAD)
        {
            Tet.maxDistance = float.PositiveInfinity;
            this.gameObject.SetActive(false);
        }

        // if player is in revive state 
        if (playerState == PlayerState.REVIVE)
        {
            // call the death timer
            DeathTimer -= Time.deltaTime;
            // spawn revive radius particle system
            if (reviveVolume != null)
            {
                ParticleSystem.MainModule revMainMod = reviveVolume.GetComponentInChildren<ParticleSystem>().main;
                revMainMod.startColor = Color.red;
                reviveVolume.GetComponentInChildren<ParticleSystem>().Play();
            }
        }
    }


    // damage to the player inflicted
    public void DamagePlayer(float amount)
    {
        currentHealth -= amount;
        print("Player health: " + currentHealth);
    }

    // player health management
    void playerHealth()
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