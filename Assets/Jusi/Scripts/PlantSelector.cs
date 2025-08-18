using UnityEngine;
using UnityEngine.UI;

public class PlantSelector : MonoBehaviour
{
    public Button orangeButton;
    public Button mangoButton;
    public Button bananaButton;
    public Button pineappleButton;

    private Button selectedButton = null;
    private string selectedTree = "";

    public static string SelectedTree { get; private set; } = "";

    void Start()
    {
        // Add listeners to each button
        orangeButton.onClick.AddListener(() => SelectTree("Orange", orangeButton));
        mangoButton.onClick.AddListener(() => SelectTree("Mango", mangoButton));
        bananaButton.onClick.AddListener(() => SelectTree("Banana", bananaButton));
        pineappleButton.onClick.AddListener(() => SelectTree("Pineapple", pineappleButton));
    }

    void Update()
    {
        // Press Escape to cancel planting mode
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeselectTree();
        }
    }

    void SelectTree(string treeName, Button button)
    {
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
}
