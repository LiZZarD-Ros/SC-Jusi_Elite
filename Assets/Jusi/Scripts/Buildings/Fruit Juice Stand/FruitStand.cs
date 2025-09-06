using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class FruitStand : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text juiceBoxInStandText;
    [SerializeField] private Image juiceDisplayImage;
    [SerializeField] private Sprite mangoJuiceSprite;
    [SerializeField] private Sprite orangeJuiceSprite;
    [SerializeField] private Sprite bananaJuiceSprite;
    [SerializeField] private Sprite pineappleJuiceSprite;

    [Header("Stand Settings")]
    [SerializeField] private Transform coinSpawnPoint;
    [SerializeField] private Animator standAnimator;
    [SerializeField] private ParticleSystem sellParticles;
    [Range(0f, 1f)][SerializeField] private float baseSellChancePerTick = 0.25f; // 25% base chance

    [Header("Prefabs")]
    [SerializeField] private GameObject goldCoinPrefab;

    private string[] juices = { "Mango", "Orange", "Banana", "Pineapple" };
    private int selectedJuiceIndex = 0;
    private int juiceBoxInStand = 0;
    private const int maxCapacity = 1;
    private bool isSelling = false;

    // robust event subscription state
    private Timing subscribedTiming;
    private Coroutine subscribeRoutine;

    // reference to building upgrader
    private UpgradeableBuilding upgradeableBuilding;

    private void Awake()
    {
        upgradeableBuilding = GetComponent<UpgradeableBuilding>();
    }

    private void OnEnable()
    {
        TrySubscribe();
        if (subscribedTiming == null)
            subscribeRoutine = StartCoroutine(WaitAndSubscribe());
    }

    private void OnDisable()
    {
        if (subscribeRoutine != null)
        {
            StopCoroutine(subscribeRoutine);
            subscribeRoutine = null;
        }

        if (subscribedTiming != null)
        {
            subscribedTiming.OnSecondTick -= OnTick;
            subscribedTiming = null;
        }
    }

    private void Start()
    {
        UpdateJuiceDisplay();
        UpdateUI();
    }

    private void TrySubscribe()
    {
        if (Timing.Instance != null && subscribedTiming == null)
        {
            Timing.Instance.OnSecondTick += OnTick;
            subscribedTiming = Timing.Instance;
        }
    }

    private IEnumerator WaitAndSubscribe()
    {
        while (Timing.Instance == null) yield return null;
        TrySubscribe();
    }

    // --- Juice Selection ---
    public void NextJuice()
    {
        if (isSelling) return;
        selectedJuiceIndex = (selectedJuiceIndex + 1) % juices.Length;
        UpdateJuiceDisplay();
    }

    public void PreviousJuice()
    {
        if (isSelling) return;
        selectedJuiceIndex = (selectedJuiceIndex - 1 + juices.Length) % juices.Length;
        UpdateJuiceDisplay();
    }

    private void UpdateJuiceDisplay()
    {
        switch (juices[selectedJuiceIndex].ToLower())
        {
            case "mango": juiceDisplayImage.sprite = mangoJuiceSprite; break;
            case "orange": juiceDisplayImage.sprite = orangeJuiceSprite; break;
            case "banana": juiceDisplayImage.sprite = bananaJuiceSprite; break;
            case "pineapple": juiceDisplayImage.sprite = pineappleJuiceSprite; break;
        }
    }

    // --- Loading / Unloading ---
    public void AddJuiceBox()
    {
        if (isSelling || juiceBoxInStand >= maxCapacity) return;

        switch (juices[selectedJuiceIndex].ToLower())
        {
            case "mango":
                if (JuiceBoxManager.Instance.GetMangoJuice() > 0)
                { JuiceBoxManager.Instance.RemoveMangoJuice(); juiceBoxInStand++; }
                break;
            case "orange":
                if (JuiceBoxManager.Instance.GetOrangeJuice() > 0)
                { JuiceBoxManager.Instance.RemoveOrangeJuice(); juiceBoxInStand++; }
                break;
            case "banana":
                if (JuiceBoxManager.Instance.GetBananaJuice() > 0)
                { JuiceBoxManager.Instance.RemoveBananaJuice(); juiceBoxInStand++; }
                break;
            case "pineapple":
                if (JuiceBoxManager.Instance.GetPineappleJuice() > 0)
                { JuiceBoxManager.Instance.RemovePineappleJuice(); juiceBoxInStand++; }
                break;
        }

        UpdateUI();
    }

    public void RemoveJuiceBox()
    {
        if (isSelling || juiceBoxInStand <= 0) return;

        juiceBoxInStand--;

        switch (juices[selectedJuiceIndex].ToLower())
        {
            case "mango": JuiceBoxManager.Instance.AddMangoJuice(); break;
            case "orange": JuiceBoxManager.Instance.AddOrangeJuice(); break;
            case "banana": JuiceBoxManager.Instance.AddBananaJuice(); break;
            case "pineapple": JuiceBoxManager.Instance.AddPineappleJuice(); break;
        }

        UpdateUI();
    }

    // --- Selling ---
    public void StartSelling()
    {
        if (isSelling || juiceBoxInStand <= 0) return;

        isSelling = true;

        if (standAnimator != null)
            standAnimator.SetBool("IsSelling", true);

        if (sellParticles != null)
            sellParticles.Play();
    }

    private void OnTick()
    {
        if (!isSelling || juiceBoxInStand <= 0) return;

        // apply upgrade modifier
        float effectiveChance = baseSellChancePerTick;
        if (upgradeableBuilding != null)
        {
            int level = upgradeableBuilding.GetLevel(); // 1–5
            effectiveChance *= (1f + (level - 1) * 0.1f);
        }

        if (Random.value < effectiveChance)
        {
            // success! sold
            juiceBoxInStand--;
            UpdateUI();
            FinishSelling();
        }
    }

    private void FinishSelling()
    {
        isSelling = false;

        if (standAnimator != null)
            standAnimator.SetBool("IsSelling", false);

        if (sellParticles != null)
            sellParticles.Stop();

        // Spawn coin
        if (goldCoinPrefab != null && coinSpawnPoint != null)
            Instantiate(goldCoinPrefab, coinSpawnPoint.position, Quaternion.identity);

        Debug.Log($"Fruit stand sold a {juices[selectedJuiceIndex]} juice box!");
    }

    private void UpdateUI()
    {
        if (juiceBoxInStandText != null)
            juiceBoxInStandText.text = $"{juiceBoxInStand}/{maxCapacity}";
    }
}
