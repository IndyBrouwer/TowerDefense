using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TowerPlacement towerPlacementScript;
    [SerializeField] private UpgradeShopController upgradeShopControllerScript;

    //Only trigger when in build phase
    public void OnLeftClick()
    {
        //Check if player clicked on UI element or gameobject
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("AttackTower") || hit.collider.gameObject.CompareTag("SuppTower"))
            {
                //Open upgrade shop menu
                upgradeShopControllerScript.OnPlayerLeftClicked();
            }
            else if (hit.collider.gameObject.CompareTag("PlacingTile"))
            {
                //Place tower if player has selected one from the shop
                towerPlacementScript.OnPlayerLeftClicked();
            }

            Debug.Log("Player left clicked on: " + hit.collider.gameObject.name);
        }
    }

    public void OnRightClick()
    {
        //If player is holding tower, cancel holding tower
        if (towerPlacementScript.selectedTower != null)
        {
            towerPlacementScript.OnPlayerRightClicked();
        }
        //If player has upgrade shop menu open, close it
        else if (upgradeShopControllerScript.upgradeShopMenu.activeSelf)
        {
            upgradeShopControllerScript.DisableShop();
        }
    }
}