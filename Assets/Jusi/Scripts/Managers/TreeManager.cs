using UnityEngine;

public class TreeManager : MonoBehaviour
{
    // Tree counts
    [SerializeField] private int mangoTrees;
    [SerializeField] private int orangeTrees;
    [SerializeField] private int palmTrees;
    [SerializeField] private int pineappleTrees;

    // Getters
    public int GetMangoTrees() { return mangoTrees; }
    public int GetOrangeTrees() { return orangeTrees; }
    public int GetPalmTrees() { return palmTrees; }
    public int GetPineappleTrees() { return pineappleTrees; }

    // Setters
    public void SetMangoTrees(int count) { mangoTrees = count; }
    public void SetOrangeTrees(int count) { orangeTrees = count; }
    public void SetPalmTrees(int count) { palmTrees = count; }
    public void SetPineappleTrees(int count) { pineappleTrees = count; }

    // Optional: increment/decrement helpers
    public void AddMangoTree(int amount = 1) { mangoTrees += amount; }
    public void AddOrangeTree(int amount = 1) { orangeTrees += amount; }
    public void AddPalmTree(int amount = 1) { palmTrees += amount; }
    public void AddPineappleTree(int amount = 1) { pineappleTrees += amount; }

    public void RemoveMangoTree(int amount = 1) { mangoTrees = Mathf.Max(0, mangoTrees - amount); }
    public void RemoveOrangeTree(int amount = 1) { orangeTrees = Mathf.Max(0, orangeTrees - amount); }
    public void RemovePalmTree(int amount = 1) { palmTrees = Mathf.Max(0, palmTrees - amount); }
    public void RemovePineappleTree(int amount = 1) { pineappleTrees = Mathf.Max(0, pineappleTrees - amount); }
}
