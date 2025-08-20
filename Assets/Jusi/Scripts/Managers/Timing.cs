using UnityEngine;
using System;

public class Timing : MonoBehaviour
{
    public static Timing Instance;
    public int modifer;

    public float TotalSeconds { get; private set; }

   public event Action OnSecondTick;

    private float secondCounter;

    void Awake()
    {
       if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        TotalSeconds += Time.deltaTime * modifer;

        
        secondCounter += Time.deltaTime * modifer;

        if (secondCounter >= 1f)
        {
            secondCounter = 0f;
            OnSecondTick?.Invoke();
        }
    }
}
