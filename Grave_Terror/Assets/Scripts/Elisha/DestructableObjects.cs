using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjects : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;
    private Renderer rend;
    //private Color colour;
    public float damageFlashLength;
    private float flashCounter;

    private void Awake()
    {
        currentHealth = maxHealth;
        //colour = rend.material.GetColor("_Color");
        rend = GetComponent<Renderer>();
    }


   public void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(obj: gameObject);
        }

        if (flashCounter > 0)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                //rend.material.SetColor("_Color", Color.grey);
            }
        }
    }

   public void ObjectDamage(int _amount)
    {
        currentHealth -= _amount;
        flashCounter = damageFlashLength;
        //rend.material.SetColor("_Color", Color.red);
        Debug.Log("Objects health: " + currentHealth);
    }
}