using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour, ICard, IInteractable
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI towerCost;
    [SerializeField] private Image towerImage;
    [SerializeField] private TextMeshProUGUI towerDescription;
    [SerializeField] private Image background;

    [SerializeField] private Color cantAffordColor;
    private TowerData currentTower;
    private int adjustedCost;

    [Header("Other Scripts")]
    [SerializeField] private TowerShopController towershopControllerScript;
    [SerializeField] private TowerPlacement towerPlacementScript;
    [SerializeField] private Wallet walletScript;
    [SerializeField] private UIShake uiShakeScript;

    public void SetCardData(TowerData data, int currentWaveIndex)
    {
        currentTower = data;
        towerName.text = data.TowerName;

        float costMultiplier = Mathf.Pow(1.05f, currentWaveIndex);
        adjustedCost = Mathf.CeilToInt(data.Cost * costMultiplier);

        towerCost.text = adjustedCost.ToString();
        towerImage.sprite = data.TowerSprite;
        towerDescription.text = data.TowerDescription;
    }

    public void OnPlayerInteract()
    {
        //Money check
        if (walletScript.GetCurrencyAmount() < adjustedCost)
        {
            //Shake card UI
            uiShakeScript.StartShake();

            //Play no funds sound
            AudioManager.Instance.sfxManager.PlaySFX(walletScript.noFundsSound);

            //Enable red screen effect for a second
            background.color = cantAffordColor;
            towerImage.color = cantAffordColor;

            StartCoroutine(ResetColor());

            Debug.LogWarning("Not enough currency to place this tower!");
            return;
        }

        //Hide the shop menu
        towershopControllerScript.DisableShop();

        towerPlacementScript.SetSelectedTower(currentTower, adjustedCost);
    }


    private IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.2f);

        background.color = Color.white;
        towerImage.color = Color.white;
    }
}