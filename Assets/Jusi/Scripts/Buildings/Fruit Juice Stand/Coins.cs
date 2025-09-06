using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int coinValue = 1; // how many coins this pickup gives

    private void OnMouseDown()
    {
        // Add coins to MoneyManager
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.AddCoins(coinValue);

        // Destroy this coin object
        Destroy(gameObject);
    }
}
