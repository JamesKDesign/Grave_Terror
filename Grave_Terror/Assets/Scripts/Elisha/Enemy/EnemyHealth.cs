using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public float currentHealth;

    private void Awake()
    {
        currentHealth = health;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            gameObject.GetComponent<Enemy>().Dead();
       
            print("enemy dead");
        }
    }

    public void DamageHealth(float amount)
    {
        currentHealth -= amount;

        print("Enemy health is " + currentHealth);
    }
}