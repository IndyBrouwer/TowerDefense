using System.Collections;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [HideInInspector] public TowerData selectedTower;
    private GameObject previewInstance;
    private GameObject placedTower;

    [SerializeField] private TowerShopController towerShopControllerScript;
    [SerializeField] private Wallet walletScript;

    public void OnPlayerLeftClicked()
    {
        //Check if shop is active and if a tower is selected
        if (selectedTower == null)
        {
            return;
        }

        PlaceTower();
    }

    public void OnPlayerRightClicked()
    {
        if (previewInstance != null)
        {
            Destroy(previewInstance);
        }

        selectedTower = null;

        //Enable shop icon again
        towerShopControllerScript.towerShopButton.SetActive(true);
    }

    public void SetSelectedTower(TowerData tower)
    {
        selectedTower = tower;

        // Create preview object
        if (previewInstance != null)
        {
            Destroy(previewInstance);
        }

        previewInstance = Instantiate(tower.TowerPrefab);

        //Disable collider for preview to avoid triggering placement issues
        previewInstance.GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        if (selectedTower == null)
        {
            return;
        }

        MovePreviewToMouse();
    }

    private void MovePreviewToMouse()
    {
        if (previewInstance == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("PlacingTile"))
            {
                previewInstance.transform.position = hit.collider.transform.position;
            }
        }
    }

    private void PlaceTower()
    {
        //Check if the player has enough resources to buy the tower
        if (walletScript.GetCurrencyAmount() < selectedTower.Cost)
        {
            Debug.LogWarning("Not enough currency to place this tower!");
            return;
        }

        //Check if its an empty tile
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit) || !hit.collider.CompareTag("PlacingTile"))
        {
            Debug.LogWarning("You can only place towers on empty tiles!");
            return;
        }
        else
        {
            //Remove cost amount from player wallet.
            walletScript.RemoveCurrency(selectedTower.Cost);

            //Remove tag from tile to prevent placing multiple towers on the same tile
            hit.collider.tag = "Untagged";

            Vector3 placePosition = hit.collider.transform.position;

            placedTower = Instantiate(selectedTower.TowerPrefab, placePosition, Quaternion.identity);

            Destroy(previewInstance);
            selectedTower = null;

            //Enable shop icon again
            towerShopControllerScript.towerShopButton.SetActive(true);
        }
    }
}