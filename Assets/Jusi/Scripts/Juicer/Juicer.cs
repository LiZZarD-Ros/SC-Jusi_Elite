using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class Juicer : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text fruitInJuicerText;
    [SerializeField] private Image fruitDisplayImage;
    [SerializeField] private Sprite mangoSprite;
    [SerializeField] private Sprite orangeSprite;
    [SerializeField] private Sprite bananaSprite;
    [SerializeField] private Sprite pineappleSprite;

    [Header("Juicer Settings")]
    [SerializeField] private Transform juiceSpawnPoint;
    [SerializeField] private Animator juicerAnimator;
    [SerializeField] private ParticleSystem juicerParticles;

    [Header("Juice Prefabs")]
    [SerializeField] private GameObject mangoJuicePrefab;
    [SerializeField] private GameObject orangeJuicePrefab;
    [SerializeField] private GameObject bananaJuicePrefab;
    [SerializeField] private GameObject pineappleJuicePrefab;

    private string[] fruits = { "Mango", "Orange", "Banana", "Pineapple" };
    private int selectedFruitIndex = 0;
    private int fruitInJuicer = 0;
    private const int maxCapacity = 5;
    private bool isJuicing = false;

    // robust event subscription state
    private Timing subscribedTiming;
    private Coroutine subscribeRoutine;

    private void OnEnable()
    {
        TrySubscribe();
        if (subscribedTiming == null) // Timing.Instance not ready yet; wait a frame(s)
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
        UpdateFruitDisplay();
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
        // wait until Timing.Instance exists (Awake has run)
        while (Timing.Instance == null) yield return null;
        TrySubscribe();
    }

    // --- Fruit Selection ---
    public void NextFruit()
    {
        if (isJuicing) return;
        selectedFruitIndex = (selectedFruitIndex + 1) % fruits.Length;
        UpdateFruitDisplay();
    }

    public void PreviousFruit()
    {
        if (isJuicing) return;
        selectedFruitIndex = (selectedFruitIndex - 1 + fruits.Length) % fruits.Length;
        UpdateFruitDisplay();
    }

    private void UpdateFruitDisplay()
    {
        switch (fruits[selectedFruitIndex].ToLower())
        {
            case "mango": fruitDisplayImage.sprite = mangoSprite; break;
            case "orange": fruitDisplayImage.sprite = orangeSprite; break;
            case "banana": fruitDisplayImage.sprite = bananaSprite; break;
            case "pineapple": fruitDisplayImage.sprite = pineappleSprite; break;
        }
    }

    // --- Loading / Unloading ---
    public void AddFruit()
    {
        if (isJuicing || fruitInJuicer >= maxCapacity) return;

        switch (fruits[selectedFruitIndex].ToLower())
        {
            case "mango":
                if (FruitManager.Instance.GetMangoes() > 0)
                { FruitManager.Instance.RemoveMango(); fruitInJuicer++; }
                break;
            case "orange":
                if (FruitManager.Instance.GetOranges() > 0)
                { FruitManager.Instance.RemoveOrange(); fruitInJuicer++; }
                break;
            case "banana":
                if (FruitManager.Instance.GetBananas() > 0)
                { FruitManager.Instance.RemoveBanana(); fruitInJuicer++; }
                break;
            case "pineapple":
                if (FruitManager.Instance.GetPineapples() > 0)
                { FruitManager.Instance.RemovePineapple(); fruitInJuicer++; }
                break;
        }

        UpdateUI();
    }

    public void RemoveFruit()
    {
        if (isJuicing || fruitInJuicer <= 0) return;

        fruitInJuicer--;

        switch (fruits[selectedFruitIndex].ToLower())
        {
            case "mango": FruitManager.Instance.AddMango(); break;
            case "orange": FruitManager.Instance.AddOrange(); break;
            case "banana": FruitManager.Instance.AddBanana(); break;
            case "pineapple": FruitManager.Instance.AddPineapple(); break;
        }

        UpdateUI();
    }

    // --- Juicing ---
    public void StartJuicing()
    {
        // NOTE: currently requires a full load. If you want to juice any amount, change to (fruitInJuicer <= 0)
        if (isJuicing || fruitInJuicer < maxCapacity) return;

        isJuicing = true;

        if (juicerAnimator != null)
            juicerAnimator.SetBool("IsJuicing", true);

        if (juicerParticles != null)
            juicerParticles.Play();
    }

    private void OnTick()
    {
        if (!isJuicing) return;

        if (fruitInJuicer > 0)
        {
            fruitInJuicer--;
            UpdateUI();
        }

        if (fruitInJuicer <= 0)
        {
            FinishJuicing();
        }
    }

    private void FinishJuicing()
    {
        isJuicing = false;

        if (juicerAnimator != null)
            juicerAnimator.SetBool("IsJuicing", false);

        if (juicerParticles != null)
            juicerParticles.Stop();

        // Pick correct juice prefab
        GameObject juiceToSpawn = null;
        switch (fruits[selectedFruitIndex].ToLower())
        {
            case "mango": juiceToSpawn = mangoJuicePrefab; break;
            case "orange": juiceToSpawn = orangeJuicePrefab; break;
            case "banana": juiceToSpawn = bananaJuicePrefab; break;
            case "pineapple": juiceToSpawn = pineappleJuicePrefab; break;
        }

        if (juiceToSpawn != null && juiceSpawnPoint != null)
            Instantiate(juiceToSpawn, juiceSpawnPoint.position, Quaternion.identity);

        Debug.Log($"Juicer finished making {fruits[selectedFruitIndex]} juice!");
    }

    private void UpdateUI()
    {
        if (fruitInJuicerText != null)
            fruitInJuicerText.text = $"{fruitInJuicer}/{maxCapacity}";
    }
}