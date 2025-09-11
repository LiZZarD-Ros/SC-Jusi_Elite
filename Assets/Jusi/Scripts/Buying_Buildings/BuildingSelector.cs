using UnityEngine;
using UnityEngine.UI;

public class BuildingSelector : MonoBehaviour
{
    public static bool IsPlacingBuilding { get; private set; } = false;

    public Button buyBuildingButton;
    public int buildingCost = 100;

    public MoneyManager goldManager; // your currency manager

    private void Start()
    {
        buyBuildingButton.onClick.AddListener(SelectBuilding);
    }

    void SelectBuilding()
    {
        if (IsPlacingBuilding)
        {
            // cancel placement if clicked again
            DeselectBuilding();
            return;
        }

        if (goldManager.GetCoins() < buildingCost)
        {
            Debug.Log("Not enough gold!");
            return;
        }

        IsPlacingBuilding = true;
        buyBuildingButton.interactable = false;

        Debug.Log("Building placement mode ON");
    }

    public void DeselectBuilding()
    {
        IsPlacingBuilding = false;
        buyBuildingButton.interactable = true;
        Debug.Log("Building placement mode OFF");
    }

    public void ConfirmPurchase()
    {
        goldManager.RemoveCoins(buildingCost);
        buyBuildingButton.interactable = true;
        IsPlacingBuilding = false;
        Debug.Log("Building purchased for " + buildingCost + " gold!");
    }
}
