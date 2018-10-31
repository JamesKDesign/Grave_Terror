using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjects : MonoBehaviour {

    public float maxHealth;
    public float currentHealth;
    private Renderer rend;
    private Color colour;
    public float damageFlashLength;
    public float flashCounter;

    private void Awake()
    {
        currentHealth = maxHealth;
        colour = rend.material.GetColor("_Color");
        rend = GetComponent<Renderer>();
    }


    void Break()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (flashCounter > 0)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                rend.material.SetColor("_Color", Color.blue);
            }
        }
    }

    void ObjectDamage(float _amount)
    {
        currentHealth -= _amount;
        // flashCounter = damageFlashLength;
        rend.material.SetColor("_Color", Color.red);
        print("Player health: " + currentHealth);
    }
}