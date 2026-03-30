using UnityEngine;

public class TowerShopController : MonoBehaviour
{
    [SerializeField] private GameObject towerShopMenu;
    [SerializeField] private GameObject towerShopButton;

    public void ToggleTowerShopMenu()
    {
        towerShopMenu.SetActive(!towerShopMenu.activeSelf);
        towerShopButton.SetActive(!towerShopButton.activeSelf);
    }
}