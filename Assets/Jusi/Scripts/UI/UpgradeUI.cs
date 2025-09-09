using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private UpgradeableBuilding selectedBuilding;

    private void Start()
    {
        upgradeButton.onClick.AddListener(OnUpgradePressed);
    }

    private void Update()
    {
        // Button is only active if there are upgrades available and a building is selected
        upgradeButton.interactable = (selectedBuilding != null && UpgradeManager.Instance.GetAvailableUpgrades() > 0);
    }

    public void SelectBuilding(UpgradeableBuilding building)
    {
        selectedBuilding = building;
    }

    private void OnUpgradePressed()
    {
        // ADD UI AUDIO:
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClickSound();
            
        if (selectedBuilding != null && UpgradeManager.Instance.UseUpgrade())
        {
            selectedBuilding.Upgrade();
        }
    }
}