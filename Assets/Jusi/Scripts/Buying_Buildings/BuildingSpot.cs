using UnityEngine;

public class BuildingSpot : MonoBehaviour
{
    [Header("Building Objects")]
    public GameObject buildingShadow;   // ghost version
    public GameObject mainBuilding;     // final version

    private bool isBuilt = false;

    private void OnMouseEnter()
    {
        if (isBuilt || !BuildingSelector.IsPlacingBuilding) return;

        buildingShadow.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (isBuilt) return;

        buildingShadow.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (isBuilt || !BuildingSelector.IsPlacingBuilding) return;

        // Place building
        buildingShadow.SetActive(false);
        mainBuilding.SetActive(true);

        isBuilt = true;

        // Deduct gold
        FindObjectOfType<BuildingSelector>().ConfirmPurchase();

        Debug.Log("Building placed!");
    }
}
