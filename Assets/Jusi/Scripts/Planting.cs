using UnityEngine;
using UnityEngine.EventSystems;

public class Planting : MonoBehaviour
{
    [Header("Tree Sprites")]
    public GameObject orangeTree;
    public GameObject mangoTree;
    public GameObject bananaTree;
    public GameObject pineappleTree;

    [Header("Selectors (Outlines)")]
    public GameObject orangeOutline;
    public GameObject mangoOutline;
    public GameObject bananaOutline;
    public GameObject pineappleOutline;

    private bool isPlanted = false;
    private bool isHovering = false;

    void OnMouseEnter()
    {
        if (isPlanted) return;

        string selectedTree = PlantSelector.SelectedTree;
        ShowOutline(selectedTree, true);
        isHovering = true;
    }

    void OnMouseExit()
    {
        if (isPlanted) return;

        string selectedTree = PlantSelector.SelectedTree;
        ShowOutline(selectedTree, false);
        isHovering = false;
    }

    void OnMouseDown()
    {
        if (isPlanted) return;

        string selectedTree = PlantSelector.SelectedTree;
        if (string.IsNullOrEmpty(selectedTree)) return;

        // Hide outline and plant the tree
        ShowOutline(selectedTree, false);
        PlantTree(selectedTree);

        isPlanted = true; // prevent re-planting
        isHovering = false;
    }

    private void ShowOutline(string treeName, bool state)
    {
        if (treeName == "Orange") orangeOutline.SetActive(state);
        if (treeName == "Mango") mangoOutline.SetActive(state);
        if (treeName == "Banana") bananaOutline.SetActive(state);
        if (treeName == "Pineapple") pineappleOutline.SetActive(state);
    }

    private void PlantTree(string treeName)
    {
        if (treeName == "Orange") orangeTree.SetActive(true);
        if (treeName == "Mango") mangoTree.SetActive(true);
        if (treeName == "Banana") bananaTree.SetActive(true);
        if (treeName == "Pineapple") pineappleTree.SetActive(true);

        Debug.Log("Planted: " + treeName);
    }
}
