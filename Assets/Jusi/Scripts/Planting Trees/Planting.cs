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

        // Check if we can actually plant this tree
        PlantSelector ui = FindObjectOfType<PlantSelector>();
        if (ui == null) return;
        if (!ui.CanPlant(selectedTree)) return;

        ShowOutline(selectedTree, true);
        isHovering = true;
    }

    void OnMouseExit()
    {
        if (isPlanted) return;

        string selectedTree = PlantSelector.SelectedTree;

        // Only hide outline if the tree could be planted
        PlantSelector ui = FindObjectOfType<PlantSelector>();
        if (ui == null) return;
        if (!ui.CanPlant(selectedTree)) return;

        ShowOutline(selectedTree, false);
        isHovering = false;
    }

    void OnMouseDown()
    {
        if (isPlanted) return;

        string selectedTree = PlantSelector.SelectedTree;
        if (string.IsNullOrEmpty(selectedTree)) return;

        // Check if enough trees available
        PlantSelector ui = FindObjectOfType<PlantSelector>();
        if (ui == null) return;

        if (!ui.CanPlant(selectedTree)) return;

        // Plant tree
        ShowOutline(selectedTree, false);
        PlantTree(selectedTree);

        isPlanted = true;
        isHovering = false;

        // Decrease tree count in manager
        ui.DecreaseTreeCount(selectedTree);
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
        // 🔊 Play planting sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayTreePlantSound();

        // 🌱 Activate the correct tree
        if (treeName == "Orange") orangeTree.SetActive(true);
        if (treeName == "Mango") mangoTree.SetActive(true);
        if (treeName == "Banana") bananaTree.SetActive(true);
        if (treeName == "Pineapple") pineappleTree.SetActive(true);

        Debug.Log("Planted: " + treeName);
    }
}
