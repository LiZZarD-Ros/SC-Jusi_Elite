using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Header("Fruit Growth Settings")]
    public string fruitType;    // "Orange", "Mango", etc.
    public int maxGrowth = 5;   // how many ticks until ripe
    public float growthScale = 0.2f; // scale per tick

    private int currentGrowth = 0;
    private bool isRipe = false;

    [HideInInspector] public FruitGrower parentGrower;

    void OnEnable()
    {
        Timing.Instance.OnSecondTick += Grow;
        transform.localScale = Vector3.zero; // start tiny
    }

    void OnDisable()
    {
        if (Timing.Instance != null)
            Timing.Instance.OnSecondTick -= Grow;
    }

    void Grow()
    {
        if (isRipe) return;

        currentGrowth++;
        transform.localScale += Vector3.one * growthScale;

        if (currentGrowth >= maxGrowth)
        {
            isRipe = true;
            Debug.Log(fruitType + " is ripe!");
        }
    }

    void OnMouseDown()
    {
        if (!isRipe) return;

        Debug.Log("Harvested " + fruitType);
        parentGrower.NotifyFruitHarvested();

        Destroy(gameObject);
    }
}
