using UnityEngine;
using TMPro;

public class UpgradeNumberUI : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TMP_Text upgradeText;

    void Update()
    {
        if (UpgradeManager.Instance == null) return;

        upgradeText.text = UpgradeManager.Instance.GetAvailableUpgrades().ToString();
    }
}
