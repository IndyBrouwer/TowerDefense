using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private Slider healthBar;
    private Image fillImage;

    public int maxHealth = 100;
    private float currentHealth;

    [SerializeField] private GameResult gameResultScript;

    private void Start()
    {
        currentHealth = maxHealth;

        fillImage = healthBar.fillRect.GetComponentInChildren<Image>();
    }

    public void TakeDamage(float amount)
    {
        //Decrease player health (UI healthbar)

        currentHealth -= amount;

        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            //Make fill from healthbar invisible on 0 health so it looks like 0 hp as well.
            Color color = fillImage.color;
            color.a = 0f;
            fillImage.color = color;

            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player base destroyed! Game Over.");

        gameResultScript.ShowDefeat();
    }
}
