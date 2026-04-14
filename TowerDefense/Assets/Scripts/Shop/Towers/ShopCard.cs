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

    private TowerData currentTower;
    private int adjustedCost;

    [Header("Other Scripts")]
    [SerializeField] private TowerShopController towershopControllerScript;
    [SerializeField] private TowerPlacement towerPlacementScript;

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
        //Hide the shop menu
        towershopControllerScript.DisableShop();

        towerPlacementScript.SetSelectedTower(currentTower, adjustedCost);
    }
}