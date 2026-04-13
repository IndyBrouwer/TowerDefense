using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float clickLockDuration = 0.5f;
    private float clickTimer = 0f;
    private bool clickLocked = false;

    private TowerAttack currentHoveredTower;

    [Header("Other Scripts")]
    [SerializeField] private TowerPlacement towerPlacementScript;
    [SerializeField] private UpgradeShopController upgradeShopControllerScript;
    [SerializeField] private GameStateManager gameStateManagerScript;

    private void Update()
    {
        if (clickLocked)
        {
            clickTimer -= Time.deltaTime;

            if (clickTimer <= 0f)
            {
                clickLocked = false;
            }
        }
    }

    //Only trigger when in build phase
    public void OnLeftClick()
    {
        if (clickLocked)
        {
            return;
        }

        //Check if player clicked on UI element or gameobject
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("AttackTower"))
            {
                TowerAttack AttackTowerScript = hit.collider.gameObject.GetComponent<TowerAttack>();

                //Open upgrade shop menu
                upgradeShopControllerScript.OnPlayerLeftClicked(AttackTowerScript);
            }
            else if (hit.collider.gameObject.CompareTag("PlacingTile"))
            {
                //Place tower if player has selected one from the shop
                towerPlacementScript.OnPlayerLeftClicked();

                clickLocked = true;
                clickTimer = clickLockDuration;
            }
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

    //Only in build mode
    public void OnTowerHover()
    {
        if (gameStateManagerScript.GetGameState() is BuildPhase)
        {
            //If the player hovers a tower, color the tower blue
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            TowerAttack newHoveredTower = null;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("AttackTower"))
                {
                    newHoveredTower = hit.collider.GetComponent<TowerAttack>();
                }

                //If player was hovering something before, and now it's different (or null)
                if (currentHoveredTower != null && currentHoveredTower != newHoveredTower)
                {
                    currentHoveredTower.ResetColor();
                }

                //Highlight the new tower
                if (newHoveredTower != null)
                {
                    newHoveredTower.HighlightTower();
                }

                //Update reference
                currentHoveredTower = newHoveredTower;
            }
        }
    }

    //public void OnTowerHold()
    //{
    //    //If player is holding tower, show range of tower
    //    if (towerPlacementScript.selectedTower != null)
    //    {
    //        towerPlacementScript.ShowRange();
    //    }
    //}
}