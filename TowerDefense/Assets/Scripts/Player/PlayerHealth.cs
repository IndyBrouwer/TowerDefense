using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private int health;

    public void TakeDamage(int amount)
    {
        //Decrease player health (UI healthbar)

        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
