using UnityEngine;
using TMPro;

public class Warehouse : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text mangoText;
    [SerializeField] private TMP_Text orangeText;
    [SerializeField] private TMP_Text bananaText;
    [SerializeField] private TMP_Text pineappleText;

    void Update()
    {
        if (FruitManager.Instance == null) return;

        mangoText.text = FruitManager.Instance.GetMangoes().ToString();
        orangeText.text = FruitManager.Instance.GetOranges().ToString();
        bananaText.text = FruitManager.Instance.GetBananas().ToString();
        pineappleText.text = FruitManager.Instance.GetPineapples().ToString();
    }
}
