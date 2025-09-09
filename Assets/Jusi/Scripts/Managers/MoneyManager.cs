using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [Header("Currency")]
    [SerializeField] private int coins;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // ---- Getter ----
    public int GetCoins() { return coins; }

    // ---- Setter ----
    public void SetCoins(int amount) { coins = amount; }

    // ---- Increment helper ----
    public void AddCoins(int amount = 1) { coins += amount; }

    // ---- Remove helper ----
    public void RemoveCoins(int amount = 1) { coins = Mathf.Max(0, coins - amount); }
}
