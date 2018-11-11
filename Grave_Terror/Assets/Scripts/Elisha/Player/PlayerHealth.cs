using UnityEngine;

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

    //public enum PlayerState
    //{
    //    ALIVE,
    //    REVIVE,
    //    DEAD
    //}

    private void Awake()
    {
        currentHealth = health;
        rend = GetComponent<Renderer>();
        colour = rend.material.GetColor("_Color");
        print("Player starting health: " + currentHealth);
    }

    private void Update()
    {
        playerHealth();
        playerFlash();
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
            Destroy(gameObject);
            //gameObject.SetActive(false);
            print("Player is dead");

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