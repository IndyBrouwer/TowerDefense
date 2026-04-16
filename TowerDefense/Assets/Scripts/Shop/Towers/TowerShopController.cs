using UnityEngine;

public class TowerShopController : MonoBehaviour
{
    public GameObject towerShopMenu;
    public GameObject towerShopButton;
    [SerializeField] private GameObject tipBox;

    public void ToggleTowerShopMenu()
    {
        towerShopMenu.SetActive(!towerShopMenu.activeSelf);
        towerShopButton.SetActive(!towerShopButton.activeSelf);

        //Hide/display tipbox based on it's activity before the toggle
        tipBox.SetActive(!tipBox.activeSelf);
    }

    public void EnableShop()
    {
        towerShopMenu.SetActive(true);
        towerShopButton.SetActive(false);

        //Hide tip box as menu is now enabled
        tipBox.SetActive(false);
    }

    public void DisableShop()
    {
        towerShopMenu.SetActive(false);
        towerShopButton.SetActive(false);

        //Show the tip box again
        tipBox.SetActive(true);
    }
}