using UnityEngine;

public class RecolorTower : MonoBehaviour
{
    [Header("Tower Parts")]
    [SerializeField] private GameObject bigCrystal;
    [SerializeField] private GameObject smallCrystals;
    private Renderer bigCrystalRenderer;
    private Renderer smallCrystalsRenderer;

    [SerializeField] private TowerData thisTower;

    private void Start()
    {
        bigCrystalRenderer = bigCrystal.GetComponent<Renderer>();
        smallCrystalsRenderer = smallCrystals.GetComponent<Renderer>();

        if (thisTower != null)
        {
            UpdateCrystalColors(thisTower);
        }
    }

    private void UpdateCrystalColors(TowerData towerData)
    {
        if (towerData == null || towerData.TowerType != "Support")
        {
            return;
        }

        Color towerColor = towerData.TowerColor;
        bigCrystalRenderer.material.color = towerColor;
        smallCrystalsRenderer.material.color = towerColor;
    }
}
