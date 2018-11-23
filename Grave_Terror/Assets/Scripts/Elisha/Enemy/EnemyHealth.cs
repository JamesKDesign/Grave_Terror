using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public float currentHealth;
    public ScoreBoard chunkKills;
    public ScoreBoard SizzleKills;

    private void Awake()
    {
        currentHealth = health;
        print("Enemy health is " + currentHealth);
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