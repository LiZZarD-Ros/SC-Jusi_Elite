using UnityEngine;

public class FruitManager : MonoBehaviour
{
    public static FruitManager Instance;

    [Header("Fruit Counts")]
    [SerializeField] private int mangoes;
    [SerializeField] private int oranges;
    [SerializeField] private int bananas;
    [SerializeField] private int pineapples;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // ---- Getters ----
    public int GetMangoes() { return mangoes; }
    public int GetOranges() { return oranges; }
    public int GetBananas() { return bananas; }
    public int GetPineapples() { return pineapples; }

    // ---- Setters ----
    public void SetMangoes(int count) { mangoes = count; }
    public void SetOranges(int count) { oranges = count; }
    public void SetBananas(int count) { bananas = count; }
    public void SetPineapples(int count) { pineapples = count; }

    // ---- Increment helpers ----
    public void AddMango(int amount = 1) { mangoes += amount; }
    public void AddOrange(int amount = 1) { oranges += amount; }
    public void AddBanana(int amount = 1) { bananas += amount; }
    public void AddPineapple(int amount = 1) { pineapples += amount; }

    // ---- Remove helpers ----
    public void RemoveMango(int amount = 1) { mangoes = Mathf.Max(0, mangoes - amount); }
    public void RemoveOrange(int amount = 1) { oranges = Mathf.Max(0, oranges - amount); }
    public void RemoveBanana(int amount = 1) { bananas = Mathf.Max(0, bananas - amount); }
    public void RemovePineapple(int amount = 1) { pineapples = Mathf.Max(0, pineapples - amount); }
}
