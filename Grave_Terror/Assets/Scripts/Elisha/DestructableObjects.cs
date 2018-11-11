using UnityEngine;

public class DestructableObjects : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;
    private Renderer rend;
    private Color colour;
    public float FlashLength;
    private float flashCounter = 1f;

    private void Awake()
    {
        currentHealth = maxHealth;
        rend = GetComponent<Renderer>();
        colour = rend.material.GetColor("_Color");
    }


   public void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            Destroy(obj: gameObject);
        }

        if (flashCounter >= 0)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                rend.material.SetColor("_Color", Color.grey);
            }
        }
    }

   public void ObjectDamage(int _amount)
    {
        currentHealth -= _amount;
        flashCounter = FlashLength;
        rend.material.SetColor("_Color", Color.red);
        Debug.Log("Objects health: " + currentHealth);
    }
}