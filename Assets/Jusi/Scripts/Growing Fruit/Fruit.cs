using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Header("Fruit Growth Settings")]
    public string fruitType;    // "Orange", "Mango", etc.
    public int maxGrowth = 5;   // how many ticks until ripe

    private float currentGrowth = 0.3f; // start with some growth
    private bool isRipe = false;

    [HideInInspector] public FruitGrower parentGrower;
    private FruitManager fruitManager;

    // Cached references
    private ParticleSystem ripeParticles;
    private Animator animator;

    void Awake()
    {
        // Grab attached components if they exist
        ripeParticles = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
    }

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

            //  Play particle effect
            if (ripeParticles != null)
                ripeParticles.Play();

            // Play animation (make sure Animator has a "Ripe" trigger or bool)
            if (animator != null)
                animator.SetTrigger("Ripe");
        }
    }

    void OnMouseDown()
    {
        if (!isRipe) return;
        
        Debug.Log("Harvested " + fruitType);
        
        // ADD HARVEST AUDIO
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayFruitHarvestSound();
            AudioManager.Instance.PlayTreeHarvestSound();
        }
        
        // Add to FruitManager
        if (FruitManager.Instance != null)
        {
            switch (fruitType.ToLower())
            {
                case "mango": FruitManager.Instance.AddMango(); break;
                case "orange": FruitManager.Instance.AddOrange(); break;
                case "banana": FruitManager.Instance.AddBanana(); break;
                case "pineapple": FruitManager.Instance.AddPineapple(); break;
            }
        }

        parentGrower.NotifyFruitHarvested();

        if (Timing.Instance != null)
            Timing.Instance.OnSecondTick -= Grow;

        Destroy(gameObject);
    }
}