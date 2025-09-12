using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TMP_Text coinText;

    void Update()
    {
        if (MoneyManager.Instance == null) return;

        coinText.text = MoneyManager.Instance.GetCoins().ToString();
    }
}
