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
        }

        Debug.Log($"{type} juice collected! Total: {GetJuiceCount(type)}");
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
            default: return 0;
        }
    }

    // Optional: Reset counts
    public void ResetCounts()
    {
        mangoJuiceCount = 0;
        orangeJuiceCount = 0;
        bananaJuiceCount = 0;
        pineappleJuiceCount = 0;
    }

    public int GetMangoes() { return mangoJuiceCount; }
    public int GetOranges() { return orangeJuiceCount; }
    public int GetBananas() { return bananaJuiceCount; }
    public int GetPineapples() { return pineappleJuiceCount; }
}