using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Shop UI")]
    [SerializeField] private Animator shopAnimator; // Animator controlling the shop UI
    [SerializeField] private string openTrigger = "Open"; // Animator trigger for opening
    [SerializeField] private string closeTrigger = "Close"; // Animator trigger for closing
    [SerializeField] private Button openButton;

    [Header("Tree Costs")]
    [SerializeField] private int mangoTreeCost = 10;
    [SerializeField] private int orangeTreeCost = 15;
    [SerializeField] private int palmTreeCost = 20;
    [SerializeField] private int pineappleTreeCost = 25;

    [Header("Upgrade Costs")]
    [SerializeField] private int upgradeCost = 50;

    private bool isShopOpen = false;

    // --- UI Controls ---
    void Start()
    {
        shopAnimator.Play("Close", 0, 1f); // Jump to the end of the Close animation
    }



    public void OpenShop()
    {
        if (isShopOpen) return; // prevents multiple opens

        isShopOpen = true;
        shopAnimator.gameObject.SetActive(true);
        shopAnimator.SetTrigger(openTrigger);
    }

    public void CloseShop()
    {
        if (!isShopOpen) return;

        shopAnimator.SetTrigger(closeTrigger);
        isShopOpen = false;
    }

    // Called via Animation Event at the end of Close animation
    public void OnCloseAnimationEnd()
    {
        shopAnimator.gameObject.SetActive(false);
        isShopOpen = false; // must reset this
    }

    // --- Buying Trees ---
    public void BuyMangoTree()
    {
        TryBuyTree(mangoTreeCost, () => TreeManager.Instance.AddMangoTree());
    }

    public void BuyOrangeTree()
    {
        TryBuyTree(orangeTreeCost, () => TreeManager.Instance.AddOrangeTree());
    }

    public void BuyPalmTree()
    {
        TryBuyTree(palmTreeCost, () => TreeManager.Instance.AddPalmTree());
    }

    public void BuyPineappleTree()
    {
        TryBuyTree(pineappleTreeCost, () => TreeManager.Instance.AddPineappleTree());
    }

    // --- Buying Upgrades ---
    public void BuyUpgrade()
    {
        TryBuy(upgradeCost, () => UpgradeManager.Instance.AddUpgrade());
    }

    // --- Helper: Trees ---
    private void TryBuyTree(int cost, System.Action onSuccess)
    {
        TryBuy(cost, onSuccess);
    }

    // --- Helper: General ---
    private void TryBuy(int cost, System.Action onSuccess)
    {
        if (MoneyManager.Instance.GetCoins() >= cost)
        {
            MoneyManager.Instance.RemoveCoins(cost);
            onSuccess?.Invoke();
            Debug.Log("Purchase successful!");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}
