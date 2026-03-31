using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour, ICard, IInteractable
{
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI towerCost;
    [SerializeField] private Image towerImage;
    [SerializeField] private TextMeshProUGUI towerDescription;

    private TowerData currentTower;

    [SerializeField] private TowerShopController towershopControllerScript;
    [SerializeField] private TowerPlacement towerPlacementScript;
    [SerializeField] private Wallet walletScript;

    public void SetCardData(TowerData data)
    {
        currentTower = data;

        towerName.text = data.TowerName;
        towerCost.text = data.Cost.ToString();
        towerImage.sprite = data.TowerSprite;
        towerDescription.text = data.TowerDescription;
    }

    public void OnPlayerInteract()
    {
        //Check if the player has enough resources to buy the tower
        walletScript.RemoveCurrency(currentTower.Cost);

        //Hide the shop menu
        towershopControllerScript.DisableShop();

        towerPlacementScript.SetSelectedTower(currentTower);
    }
}