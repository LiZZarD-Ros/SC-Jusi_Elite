using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Header("Fruit Growth Settings")]
    public string fruitType;    // "Orange", "Mango", etc.
    public int maxGrowth = 5;   // how many ticks until ripe

    private float currentGrowth = 0.3f; // start with some growth
    private bool isRipe = false;

    [HideInInspector] public FruitGrower parentGrower;

    void OnEnable()
    {
        Timing.Instance.OnSecondTick += Grow;

        // Apply starting scale based on currentGrowth
        float progress = Mathf.Clamp01(currentGrowth / maxGrowth);
        transform.localScale = Vector3.one * progress;
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

        // Growth progress between 0 and 1
        float progress = Mathf.Clamp01(currentGrowth / maxGrowth);

        // Scale smoothly from starting scale → 1
        transform.localScale = Vector3.one * progress;

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

        // Unsubscribe first to prevent tick loop issues
        if (Timing.Instance != null)
            Timing.Instance.OnSecondTick -= Grow;

        Destroy(gameObject);
    }
}
