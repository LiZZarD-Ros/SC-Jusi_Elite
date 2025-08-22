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

        mangoText.text = JuiceBoxManager.Instance.GetMangoJuice().ToString();
        orangeText.text = JuiceBoxManager.Instance.GetOrangeJuice().ToString();
        bananaText.text = JuiceBoxManager.Instance.GetBananaJuice().ToString();
        pineappleText.text = JuiceBoxManager.Instance.GetPineappleJuice().ToString();
    }


}
