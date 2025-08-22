using UnityEngine;

public class JuiceBoxManager : MonoBehaviour
{
    public static JuiceBoxManager Instance { get; private set; }

    [SerializeField] private int mangoJuiceCount = 0;
    [SerializeField] private int orangeJuiceCount = 0;
    [SerializeField] private int bananaJuiceCount = 0;
    [SerializeField] private int pineappleJuiceCount = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // --- Add Juice ---
    public void AddJuice(string type)
    {
        switch (type.ToLower())
        {
            case "mango": mangoJuiceCount++; break;
            case "orange": orangeJuiceCount++; break;
            case "banana": bananaJuiceCount++; break;
            case "pineapple": pineappleJuiceCount++; break;
            default: Debug.LogWarning($"Unknown juice type: {type}"); break;
        }

        Debug.Log($"{type} juice collected! Total: {GetJuiceCount(type)}");
    }

    // --- Remove Juice ---
    public void RemoveJuice(string type)
    {
        switch (type.ToLower())
        {
            case "mango": if (mangoJuiceCount > 0) mangoJuiceCount--; break;
            case "orange": if (orangeJuiceCount > 0) orangeJuiceCount--; break;
            case "banana": if (bananaJuiceCount > 0) bananaJuiceCount--; break;
            case "pineapple": if (pineappleJuiceCount > 0) pineappleJuiceCount--; break;
            default: Debug.LogWarning($"Unknown juice type: {type}"); break;
        }
    }

    // --- Has Juice? ---
    public bool HasJuice(string type)
    {
        return GetJuiceCount(type) > 0;
    }

    // --- Get Juice Count ---
    public int GetJuiceCount(string type)
    {
        switch (type.ToLower())
        {
            case "mango": return mangoJuiceCount;
            case "orange": return orangeJuiceCount;
            case "banana": return bananaJuiceCount;
            case "pineapple": return pineappleJuiceCount;
            default: Debug.LogWarning($"Unknown juice type: {type}"); return 0;
        }
    }

    // --- Reset Counts (optional) ---
    public void ResetCounts()
    {
        mangoJuiceCount = 0;
        orangeJuiceCount = 0;
        bananaJuiceCount = 0;
        pineappleJuiceCount = 0;
    }

    public int GetMangoJuice() { return mangoJuiceCount; }
    public int GetOrangeJuice() { return orangeJuiceCount; }
    public int GetBananaJuice() { return bananaJuiceCount; }
    public int GetPineappleJuice() { return pineappleJuiceCount; }

    public void AddMangoJuice() { mangoJuiceCount++; }
    public void AddOrangeJuice() { orangeJuiceCount++; }
    public void AddBananaJuice() { bananaJuiceCount++; }
    public void AddPineappleJuice() { pineappleJuiceCount++; }

    // --- Removers ---
    public void RemoveMangoJuice() { if (mangoJuiceCount > 0) mangoJuiceCount--; }
    public void RemoveOrangeJuice() { if (orangeJuiceCount > 0) orangeJuiceCount--; }
    public void RemoveBananaJuice() { if (bananaJuiceCount > 0) bananaJuiceCount--; }
    public void RemovePineappleJuice() { if (pineappleJuiceCount > 0) pineappleJuiceCount--; }
}