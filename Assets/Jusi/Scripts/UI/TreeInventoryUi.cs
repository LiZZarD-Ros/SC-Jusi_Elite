using UnityEngine;
using TMPro;

public class TreeInventoryUi : MonoBehaviour
{
    [SerializeField] private TreeManager treeManager;

    [Header("UI Texts")]
    [SerializeField] private TMP_Text mangoText;
    [SerializeField] private TMP_Text orangeText;
    [SerializeField] private TMP_Text palmText;
    [SerializeField] private TMP_Text pineappleText;

    void Update()
    {
        if (treeManager == null) return;

        mangoText.text = treeManager.GetMangoTrees().ToString();
        orangeText.text = treeManager.GetOrangeTrees().ToString();
        palmText.text = treeManager.GetPalmTrees().ToString();
        pineappleText.text = treeManager.GetPineappleTrees().ToString();
    }
}
