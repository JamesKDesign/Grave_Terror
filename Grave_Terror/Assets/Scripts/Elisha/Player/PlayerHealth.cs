﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float health;
    public float currentHealth;
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
        rend = GetComponent<Renderer>();
        colour = rend.material.GetColor("_Color");
        currentHealth = health;
        print("Player starting health: " + currentHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            print("Player is dead");
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

    public void DamagePlayer(float amount)
    {
        currentHealth -= amount;
        flashCounter = damageFlashLength;
        rend.material.SetColor("_Color", Color.red);
        print("Player health: " + currentHealth);
    }
}