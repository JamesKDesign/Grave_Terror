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
    [HideInInspector]
    public bool isReviving = false;

    //public AudioSource ChunkaudioSource;
    //public AudioSource SizzleaudioSource;

    //public AudioSource ChunkdeathSource;
    //public AudioSource SizzledeathSource;
    [HideInInspector]
    public bool chunkDowned = false;
    [HideInInspector]
    public bool sizzleDowned = false;
    public DynamicCamera camera;

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
                chunkDowned = false;
                sizzleDowned = false;
                anim.SetBool("IsDowned", false);
                DeathTimer = 20f;
                controls.Move();
                controls.Turning();
               // controls.Dashing();
                break;

            // Player revive state can rotate player and shoot 
            case PlayerState.REVIVE:
                print("In revive state");
                anim.SetBool("IsDowned", true);
                //TODO
                //ChunkaudioSource.Play();

                //ChunkaudioSource.clip = chunkReviveClip;
                //ChunkaudioSource.playOnAwake = false;
                //ChunkaudioSource.Play()
                break;

            // Player dead state sets player to in-active
            case PlayerState.DEAD:
                if(camera.players.Count > 0)
                {
                    camera.players.Remove(this.gameObject.transform);
                }
                //TODO
               // ChunkdeathSource.Play();

                //ChunkaudioSource.clip = chunkReviveClip;
                //ChunkaudioSource.playOnAwake = false;
                //ChunkaudioSource.Play()
                print("In death state");
                break;
        }

        // checks if both players are in dead state to then spawn game over screen
        if (player2.GetComponent<PlayerHealth>().playerState == PlayerState.DEAD && playerState == PlayerState.DEAD)
        {
            DeathScreen.SetActive(true);
            player2.gameObject.SetActive(false);
            gameObject.SetActive(false);
            Time.timeScale = 0f;
        }

        // checks if both players are in revive state to spawn game over screen
        if(player2.GetComponent<PlayerHealth>().playerState == PlayerState.REVIVE && playerState == PlayerState.REVIVE)
        {
            DeathScreen.SetActive(true);
            player2.gameObject.SetActive(false);
            gameObject.SetActive(false);
            Time.timeScale = 0f;
        }
        // checks if one player is in dead state and if the other is in revive state to then spawn game over screen
        else if(player2.GetComponent<PlayerHealth>().playerState == PlayerState.DEAD && playerState == PlayerState.REVIVE)
        {
            DeathScreen.SetActive(true);
            gameObject.SetActive(false);
            Time.timeScale = 0f;
        }

        // if one player is dead then deactivate tether restriction
        if (playerState == PlayerState.DEAD)
        {
            Tet.maxDistance = float.PositiveInfinity;
            this.gameObject.SetActive(false);
        }

        // if player is in revive state 
        if (playerState == PlayerState.REVIVE && isReviving == false)
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
            if (DeathTimer <= 0.0f)
                playerState = PlayerState.DEAD;
            else
                playerState = PlayerState.REVIVE;
        }
        else
        {
            playerState = PlayerState.ALIVE;
        }
    }
}