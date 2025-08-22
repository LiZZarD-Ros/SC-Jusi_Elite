using UnityEngine;
using TMPro;

public class Juice_Box_Warehouse : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text mangoText;
    [SerializeField] private TMP_Text orangeText;
    [SerializeField] private TMP_Text bananaText;
    [SerializeField] private TMP_Text pineappleText;

    void Update()
    {
        if (JuiceBoxManager.Instance == null) return;

        mangoText.text = JuiceBoxManager.Instance.GetMangoes().ToString();
        orangeText.text = JuiceBoxManager.Instance.GetOranges().ToString();
        bananaText.text = JuiceBoxManager.Instance.GetBananas().ToString();
        pineappleText.text = JuiceBoxManager.Instance.GetPineapples().ToString();
    }
}
