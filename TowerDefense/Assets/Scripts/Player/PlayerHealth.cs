using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private Slider healthBar;

    public int maxHealth = 100;
    private float currentHealth;

    [SerializeField] private GameResult gameResultScript;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        //Decrease player health (UI healthbar)

        currentHealth -= amount;

        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player base destroyed! Game Over.");

        gameResultScript.ShowDefeat();
    }
}
