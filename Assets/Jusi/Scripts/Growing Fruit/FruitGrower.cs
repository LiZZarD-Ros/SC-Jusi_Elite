using UnityEngine;

public class FruitGrower : MonoBehaviour
{
    [Header("Fruit Settings")]
    public GameObject fruitPrefab;           // prefab of the fruit to spawn
    public Transform[] fruitSpawnPoints;     // spawn locations for this tree
    public int maxFruitsAtOnce = 3;          // how many fruits can exist at once
    public int totalFruitToGrow = 10;        // total fruit this tree can produce
    [Range(0f, 1f)] public float spawnChance = 0.2f; // chance per tick to spawn

    private int fruitsSpawnedTotal = 0;

    void OnEnable()
    {
        Timing.Instance.OnSecondTick += HandleSecondTick;
    }

    void OnDisable()
    {
        if (Timing.Instance != null)
            Timing.Instance.OnSecondTick -= HandleSecondTick;
    }

    void HandleSecondTick()
    {
        if (fruitsSpawnedTotal >= totalFruitToGrow) return;

        // Count how many fruits are currently active
        int activeFruits = 0;
        foreach (Transform spot in fruitSpawnPoints)
        {
            if (spot.childCount > 0) activeFruits++;
        }

        if (activeFruits >= maxFruitsAtOnce) return;

        // Random chance to spawn
        if (Random.value < spawnChance)
        {
            TrySpawnFruit();
        }
    }

    void TrySpawnFruit()
    {
        foreach (Transform spot in fruitSpawnPoints)
        {
            if (spot.childCount == 0) // free spot
            {
                // 🔊 Play tree growing sound (10% chance)
                if (AudioManager.Instance != null && Random.Range(0f, 1f) < 0.1f)
                    AudioManager.Instance.PlayTreeGrowingSound();

                GameObject fruit = Instantiate(fruitPrefab, spot.position, Quaternion.identity, spot);
                Fruit fruitScript = fruit.GetComponent<Fruit>();
                if (fruitScript != null)
                {
                    fruitScript.parentGrower = this; // link back
                }

                fruitsSpawnedTotal++;
                break;
            }
        }
    }

    // Called by fruit when harvested, to free up the spawn point
    public void NotifyFruitHarvested()
    {
        // nothing special yet, but we could track stats here
    }
}
