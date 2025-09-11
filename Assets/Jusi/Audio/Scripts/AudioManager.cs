using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Central Audio Manager for Safari City Jusi
/// Handles all music and sound effects for the farming game
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource ambientSource;
    
    [Header("Background Music")]
    [SerializeField] private AudioClip farmBackgroundMusic;
    [Range(0f, 1f)]
    [SerializeField] private float musicVolume = 0.7f;
    
    [Header("Tree Sounds")]
    [SerializeField] private AudioClip treePlantSound;
    [SerializeField] private AudioClip treeGrowingSound;
    [SerializeField] private AudioClip treeHarvestSound;
    
    [Header("UI Sounds")]
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip purchaseSuccessSound;
    [SerializeField] private AudioClip purchaseFailSound;
    [SerializeField] private AudioClip shopOpenSound;
    
    [Header("Ambient Sounds")]
    [SerializeField] private AudioClip[] ambientSounds; // Birds, wind, etc.
    [SerializeField] private float ambientVolume = 0.3f;
    
    [Header("Fruit & Juice Sounds")]
    [SerializeField] private AudioClip fruitHarvestSound;
    [SerializeField] private AudioClip juicingStartSound;
    [SerializeField] private AudioClip juicingCompleteSound;
    [SerializeField] private AudioClip stallSaleSound;
    
    [Header("Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float masterVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float sfxVolume = 0.8f;
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        StartBackgroundMusic();
        StartAmbientSounds();
    }
    
    private void InitializeAudio()
    {
        // Set up audio sources if not assigned
        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();
        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();
        if (ambientSource == null)
            ambientSource = gameObject.AddComponent<AudioSource>();
            
        // Configure music source
        musicSource.loop = true;
        musicSource.volume = musicVolume * masterVolume;
        musicSource.playOnAwake = false;
        
        // Configure SFX source
        sfxSource.loop = false;
        sfxSource.volume = sfxVolume * masterVolume;
        sfxSource.playOnAwake = false;
        
        // Configure ambient source
        ambientSource.loop = true;
        ambientSource.volume = ambientVolume * masterVolume;
        ambientSource.playOnAwake = false;
    }
    
    #region Music - #204
    public void StartBackgroundMusic()
    {
        if (farmBackgroundMusic != null && !musicSource.isPlaying)
        {
            musicSource.clip = farmBackgroundMusic;
            musicSource.Play();
        }
    }
    
    public void StopBackgroundMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume * masterVolume;
    }
    #endregion
    
    #region Tree Sounds - #205
    public void PlayTreePlantSound()
    {
        PlaySFX(treePlantSound);
    }
    
    public void PlayTreeGrowingSound()
    {
        PlaySFX(treeGrowingSound);
    }
    
    public void PlayTreeHarvestSound()
    {
        PlaySFX(treeHarvestSound);
    }
    #endregion
    
    #region UI Sounds - #206
    public void PlayButtonClickSound()
    {
        PlaySFX(buttonClickSound);
    }
    
    public void PlayPurchaseSuccessSound()
    {
        PlaySFX(purchaseSuccessSound);
    }
    
    public void PlayPurchaseFailSound()
    {
        PlaySFX(purchaseFailSound);
    }
    
    public void PlayShopOpenSound()
    {
        PlaySFX(shopOpenSound);
    }
    #endregion
    
    #region Ambient Sounds - #207
    public void StartAmbientSounds()
    {
        if (ambientSounds.Length > 0)
        {
            StartCoroutine(PlayRandomAmbientSounds());
        }
    }
    
    private IEnumerator PlayRandomAmbientSounds()
    {
        while (true)
        {
            // Wait random time between ambient sounds (10-30 seconds)
            yield return new WaitForSeconds(Random.Range(10f, 30f));
            
            if (ambientSounds.Length > 0)
            {
                AudioClip randomAmbient = ambientSounds[Random.Range(0, ambientSounds.Length)];
                ambientSource.PlayOneShot(randomAmbient, ambientVolume * masterVolume);
            }
        }
    }
    #endregion
    
    #region Fruit Sounds - #208
    public void PlayFruitHarvestSound()
    {
        PlaySFX(fruitHarvestSound);
    }
    
    public void PlayJuicingStartSound()
    {
        PlaySFX(juicingStartSound);
    }
    
    public void PlayJuicingCompleteSound()
    {
        PlaySFX(juicingCompleteSound);
    }
    
    public void PlayStallSaleSound()
    {
        PlaySFX(stallSaleSound);
    }
    #endregion
    
    #region General Audio Methods
    private void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, sfxVolume * masterVolume);
        }
    }
    
    public void StopSFX()
    {
        if (sfxSource != null && sfxSource.isPlaying)
            sfxSource.Stop();
    }
    
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        UpdateAllVolumes();
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        sfxSource.volume = sfxVolume * masterVolume;
    }
    
    private void UpdateAllVolumes()
    {
        musicSource.volume = musicVolume * masterVolume;
        sfxSource.volume = sfxVolume * masterVolume;
        ambientSource.volume = ambientVolume * masterVolume;
    }
    
    public void MuteAll(bool mute)
    {
        musicSource.mute = mute;
        sfxSource.mute = mute;
        ambientSource.mute = mute;
    }
    #endregion
}

// Example integration with existing Tree script
public class TreeAudioIntegration : MonoBehaviour
{
    // Add this to your existing Tree script
    
    public void OnTreePlanted()
    {
        // Call this when a tree is planted
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayTreePlantSound();
        }
    }
    
    public void OnFruitHarvested()
    {
        // Call this when fruit is harvested
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayFruitHarvestSound();
            AudioManager.Instance.PlayTreeHarvestSound();
        }
    }
    
    public void OnTreeGrowthTick()
    {
        // Call this occasionally when tree grows (maybe every few ticks)
        if (AudioManager.Instance != null && Random.Range(0f, 1f) < 0.1f) // 10% chance per tick
        {
            AudioManager.Instance.PlayTreeGrowingSound();
        }
    }
}

// Example UI Button integration
public class UIAudioIntegration : MonoBehaviour
{
    // Add this to your UI buttons
    
    public void OnButtonClick()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayButtonClickSound();
        }
    }
    
    public void OnShopOpen()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayShopOpenSound();
        }
    }
    
    public void OnPurchaseAttempt(bool success)
    {
        if (AudioManager.Instance != null)
        {
            if (success)
                AudioManager.Instance.PlayPurchaseSuccessSound();
            else
                AudioManager.Instance.PlayPurchaseFailSound();
        }
    }
}