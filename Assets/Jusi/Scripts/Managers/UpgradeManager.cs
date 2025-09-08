using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance; // Singleton for easy access

    [SerializeField] private int availableUpgrades = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Getter
    public int GetAvailableUpgrades()
    {
        return availableUpgrades;
    }

    // Setter
    public void SetAvailableUpgrades(int value)
    {
        availableUpgrades = Mathf.Max(0, value);
    }

    // Adder
    public void AddUpgrade(int amount = 1)
    {
        availableUpgrades += amount;
    }

    // Subtractor
    public bool UseUpgrade(int amount = 1)
    {
        if (availableUpgrades >= amount)
        {
            availableUpgrades -= amount;
            return true;
        }
        return false;
    }
}
