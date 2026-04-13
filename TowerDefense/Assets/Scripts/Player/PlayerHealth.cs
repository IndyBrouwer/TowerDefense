using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private Slider healthBar;
    private Image fillImage;

    public int maxHealth = 100;
    private float currentHealth;

    [Header("Damage Flash Effect")]
    private Renderer[] renderers;
    private Color[] originalColors;
    public Color damageColor = Color.red;
    public float flashDuration = 0.25f;

    [SerializeField] private GameResult gameResultScript;
    [SerializeField] private EnemySpawning enemySpawningScript;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();

        originalColors = new Color[renderers.Length];
        for (int index = 0; index < renderers.Length; index++)
        {
            originalColors[index] = renderers[index].material.color;
        }
    }

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

        if (renderers != null && renderers.Length > 0)
        {
            StartCoroutine(FlashRed());
        }

        if (currentHealth <= 0)
        {
            //Make fill from healthbar invisible on 0 health so it looks like 0 hp as well.
            Color color = fillImage.color;
            color.a = 0f;
            fillImage.color = color;

            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        //Change the color of all renderers to damageColor
        for (int index = 0; index < renderers.Length; index++)
        {
            renderers[index].material.color = damageColor;
        }

        yield return new WaitForSeconds(flashDuration);

        //Revert the color of all renderers to their original colors
        for (int index = 0; index < renderers.Length; index++)
        {
            renderers[index].material.color = originalColors[index];
        }
    }

    private void Die()
    {
        Debug.Log("Player base destroyed! Game Over.");

        enemySpawningScript.canSpawn = false;

        gameResultScript.ShowDefeat();
    }
}
