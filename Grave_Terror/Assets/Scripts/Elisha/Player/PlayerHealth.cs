using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Tooltip("Players health amount")]
    public float health;
    [Tooltip("The players current health state")]
    public float currentHealth;
    [Tooltip("How long the flash will run for when the player is hit")]
    public float damageFlashLength;
    private float flashCounter;
    private Renderer rend;
    private Color colour;
    public float timer;
    PlayerMovement controls;
    public GameObject reviveVolume;

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
        rend = GetComponent<Renderer>();
        colour = rend.material.GetColor("_Color");
        print("Player starting health: " + currentHealth);
        controls = GetComponent<PlayerMovement>();
    }

    public void Update()
    {
        playerHealth();
        playerFlash();

        // Player state switch
        switch(playerState)
        {
            // Player alive with full functions
            case PlayerState.ALIVE:
                timer = 30f;
                controls.Dashing();
                controls.Turning();
                print("In alive state");
                break;
            // Player revive state can rotate player and shoot 
            case PlayerState.REVIVE:
                print("In revive state");
                // death timer hits 0 will kill the player 
                if (timer == 0.0f)
                {
                    gameObject.SetActive(false);
                    
                }
                break;
            // Player dead state sets player to in-active
            case PlayerState.DEAD:

                //gameObject.SetActive(false);
                Destroy(gameObject);
                reviveVolume.GetComponentInChildren<ParticleSystem>().Stop();
                print("In death state");
                break;
        }

        if(playerState == PlayerState.REVIVE)
        {
            timer -= Time.deltaTime;
            if (reviveVolume != null)
            {
                ParticleSystem.MainModule revMainMod = reviveVolume.GetComponentInChildren<ParticleSystem>().main;
                revMainMod.startColor = Color.red;
                reviveVolume.GetComponentInChildren<ParticleSystem>().Play();
            }
        }

        if (timer <= 0)
        {
            timer = 0.0f;
            playerState = PlayerState.DEAD;
        }
    }

    public void DamagePlayer(float amount)
    {
        currentHealth -= amount;
        flashCounter = damageFlashLength;
        rend.material.SetColor("_Color", Color.red);
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

    void playerFlash()
    {
        if (flashCounter > 0)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                rend.material.SetColor("_Color", Color.blue);
            }
        }
    }
}