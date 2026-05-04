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
    [SerializeField] private PauseMenu pauseMenuScript;

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

    public void OnEscapeClick()
    {
        //Call function that brings up pause menu
        pauseMenuScript.TogglePause();
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
            if (hit.collider.gameObject.CompareTag("AttackTower") && !towerPlacementScript.placingTower)
            {
                TowerAttack AttackTowerScript = hit.collider.gameObject.GetComponent<TowerAttack>();

                //Open upgrade shop menu
                upgradeShopControllerScript.OnPlayerLeftClicked(AttackTowerScript, towerPlacementScript);
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

    //Only in build mode, exclude tower if it is in the process of being placed
    public void OnTowerHover()
    {
        if (gameStateManagerScript.GetGameState() is BuildPhase)
        {
            Camera cam = Camera.main;
            if (cam == null)
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            TowerAttack newHoveredTower = null;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("AttackTower"))
                {
                    //Check if the tower hit is being placed, if yes cancel this function
                    Renderer[] hitRenderers = hit.collider.gameObject.GetComponentsInChildren<Renderer>();
                    foreach (Renderer renderer in hitRenderers)
                    {
                        if (renderer.material == towerPlacementScript.hologramMat)
                        {
                            return;
                        }
                    }

                    newHoveredTower = hit.collider.GetComponent<TowerAttack>();
                }

                //If player was hovering something before, and now it's different (or null), reset color
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

    public void OnGameStateChanged()
    {
        if (!(gameStateManagerScript.GetGameState() is BuildPhase))
        {
            if (currentHoveredTower != null)
            {
                currentHoveredTower.ResetColor();
                currentHoveredTower = null;
            }
        }
    }
}