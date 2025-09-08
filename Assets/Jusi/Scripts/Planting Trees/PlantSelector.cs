using UnityEngine;
using UnityEngine.UI;

public class PlantSelector : MonoBehaviour
{
    public static string SelectedTree { get; private set; } = "";

    public Button orangeButton;
    public Button mangoButton;
    public Button bananaButton;
    public Button pineappleButton;

    public TreeManager treeManager;

    private Button selectedButton = null;

    void Start()
    {
        // Add listeners
        orangeButton.onClick.AddListener(() => SelectTree("Orange", orangeButton));
        mangoButton.onClick.AddListener(() => SelectTree("Mango", mangoButton));
        bananaButton.onClick.AddListener(() => SelectTree("Banana", bananaButton));
        pineappleButton.onClick.AddListener(() => SelectTree("Pineapple", pineappleButton));

        UpdateButtonStates();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeselectTree();
        }
    }

    void SelectTree(string treeName, Button button)
    {
        // Prevent selecting a tree with 0 count
        if (!CanPlant(treeName)) return;

        if (selectedButton == button)
        {
            DeselectTree();
            return;
        }

        if (selectedButton != null)
            selectedButton.interactable = true;

        selectedButton = button;
        SelectedTree = treeName;
        button.interactable = false;

        Debug.Log("Planting mode: " + SelectedTree);
    }

    void DeselectTree()
    {
        if (selectedButton != null)
            selectedButton.interactable = true;

        selectedButton = null;
        SelectedTree = "";
        Debug.Log("Planting mode cancelled");
    }

    public void UpdateButtonStates()
    {
        orangeButton.interactable = treeManager.GetOrangeTrees() > 0;
        mangoButton.interactable = treeManager.GetMangoTrees() > 0;
        bananaButton.interactable = treeManager.GetPalmTrees() > 0; 
        pineappleButton.interactable = treeManager.GetPineappleTrees() > 0;
    }

    public bool CanPlant(string treeName)
    {
        if (treeName == "Orange") return treeManager.GetOrangeTrees() > 0;
        if (treeName == "Mango") return treeManager.GetMangoTrees() > 0;
        if (treeName == "Banana") return treeManager.GetPalmTrees() > 0;
        if (treeName == "Pineapple") return treeManager.GetPineappleTrees() > 0;
        return false;
    }

    public void DecreaseTreeCount(string treeName)
    {
        if (treeName == "Orange") treeManager.RemoveOrangeTree();
        if (treeName == "Mango") treeManager.RemoveMangoTree();
        if (treeName == "Banana") treeManager.RemovePalmTree();
        if (treeName == "Pineapple") treeManager.RemovePineappleTree();

        UpdateButtonStates(); // update buttons after planting
    }
}
