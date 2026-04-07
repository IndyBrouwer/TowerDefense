using UnityEngine;

public class UpgradeShopController : MonoBehaviour
{
    public GameObject upgradeShopMenu;

    public void OnPlayerLeftClicked()
    {
        //Check which gameobject was clicked
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("AttackTower") || hit.collider.gameObject.CompareTag("SuppTower"))
            {
                //Update shop menu with the tower's upgrade options

                //Enable the shop menu at mouse position, if on top half of the screen place it below the mouse, if on bottom half place it above the mouse

                EnableShop();
            }
        }
    }

    public void OnPlayerRightClicked()
    {
        if (upgradeShopMenu.activeSelf == true)
        {
            DisableShop();
        }
    }

    public void ToggleTowerShopMenu()
    {
        upgradeShopMenu.SetActive(!upgradeShopMenu.activeSelf);
    }

    public void EnableShop()
    {
        upgradeShopMenu.SetActive(true);
    }

    public void DisableShop()
    {
        upgradeShopMenu.SetActive(false);
    }
}