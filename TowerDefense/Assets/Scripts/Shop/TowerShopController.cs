using UnityEngine;

public class TowerShopController : MonoBehaviour
{
    public GameObject towerShopMenu;
    public GameObject towerShopButton;

    public void ToggleTowerShopMenu()
    {
        towerShopMenu.SetActive(!towerShopMenu.activeSelf);
        towerShopButton.SetActive(!towerShopButton.activeSelf);
    }

    public void DisableShop()
    {
        towerShopMenu.SetActive(false);
        towerShopButton.SetActive(false);
    }
}