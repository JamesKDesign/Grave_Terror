using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public float currentHealth;

    private void Awake()
    {
        currentHealth = health;
        print("Enemy health is " + currentHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
           // gameObject.GetComponent<Enemy>().Dead();
            print("enemy dead");
        }
    }

    public void DamageHealth(float amount)
    {
        currentHealth -= amount;
        print("Enemy health is " + currentHealth);
    }
}