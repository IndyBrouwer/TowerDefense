using System.Collections;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [HideInInspector] public TowerData selectedTower;
    private GameObject previewInstance;
    private GameObject placedTower;
    private int increasedCost;

    public bool placingTower = false;

    [Header("Hologram Effect")]
    [HideInInspector] public Material[] originalMaterials;
    public Material hologramMat;
    
    [SerializeField] private Material rangeMat;
    private Renderer[] renderers;

    [Header("Other Scripts")]
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

        placingTower = false;
    }

    public void SetSelectedTower(TowerData tower, int adjustedCost)
    {
        //Save cost changes
        increasedCost = adjustedCost;

        selectedTower = tower;

        // Create preview object
        if (previewInstance != null)
        {
            Destroy(previewInstance);
        }

        previewInstance = Instantiate(tower.TowerPrefab);

        //Disable collider for preview to avoid triggering placement issues
        previewInstance.GetComponent<Collider>().enabled = false;

        //Disable towerattack script for the preview to avoid errors about colors
        TowerAttack towerAttackScript = previewInstance.GetComponent<TowerAttack>();
        if (towerAttackScript != null)
        {
            towerAttackScript.enabled = false;
        }

        //Change material to hologram material
        ApplyHologramEffect();

        CreateRangeSphere();

        //Put preview on another layer to avoid raycast interacting with it
        previewInstance.layer = LayerMask.NameToLayer("Preview");
        foreach (Transform child in previewInstance.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Preview");
        }       
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
        if (previewInstance == null)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Every layer EXCEPT the Preview layer
        int layerMask = ~LayerMask.GetMask("Preview");

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.CompareTag("PlacingTile"))
            {
                previewInstance.transform.position = hit.collider.transform.position;
            }
        }
    }

    private void CreateRangeSphere()
    {
        //Create sphere to display range
        GameObject rangeIndicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        //Destory the collider that it comes with
        Destroy(rangeIndicator.GetComponent<Collider>());

        //Set sphere as child object from the preview tower
        rangeIndicator.transform.SetParent(previewInstance.transform);

        //Set position and rotatopn of the sphere
        rangeIndicator.transform.localPosition = Vector3.zero;
        rangeIndicator.transform.localRotation = Quaternion.identity;

        //Set size to range of tower
        float range = selectedTower.Range;
        rangeIndicator.transform.localScale = new Vector3(range * 2f, range * 2f, range * 2f);

        ApplyRangeProjection(rangeIndicator);
    }

    private void ApplyRangeProjection(GameObject rangeIndicator)
    {
        Renderer renderer = rangeIndicator.GetComponent<Renderer>();

        renderer.material = rangeMat;
    }

    private void ApplyHologramEffect()
    {
        renderers = previewInstance.GetComponentsInChildren<Renderer>();

        originalMaterials = new Material[renderers.Length];
        for (int index = 0; index < renderers.Length; index++)
        {
            originalMaterials[index] = renderers[index].material;
        }

        //Change all materials to hologram material
        for (int index = 0; index < renderers.Length; index++)
        {
            renderers[index].material = hologramMat;
        }
    }

    private void PlaceTower()
    {
        //Check if the player has enough resources to buy the tower
        if (walletScript.GetCurrencyAmount() < increasedCost)
        {
            Debug.LogWarning("Not enough currency to place this tower!");
            return;
        }

        //Check if its an empty tile
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Every layer EXCEPT the Preview layer
        int layerMask = ~LayerMask.GetMask("Preview");

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask) || !hit.collider.CompareTag("PlacingTile"))
        {
            Debug.LogWarning("You can only place towers on empty tiles!");
            //Play error sound effect
            return;
        }
        else
        {
            //Remove cost amount from player wallet.
            walletScript.RemoveCurrency(increasedCost);

            //Remove tag from tile to prevent placing multiple towers on the same tile
            hit.collider.tag = "Untagged";

            Vector3 placePosition = hit.collider.transform.position;

            placedTower = Instantiate(selectedTower.TowerPrefab, placePosition, Quaternion.identity);

            Destroy(previewInstance);
            selectedTower = null;

            //Enable shop icon again
            towerShopControllerScript.towerShopButton.SetActive(true);

            placingTower = false;
        }
    }
}