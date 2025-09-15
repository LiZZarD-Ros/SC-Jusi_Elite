using UnityEngine;
using DG.Tweening;

public class SimpleBreathingAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float breathingDuration = 2f;
    [SerializeField] private float breathingStrength = 0.05f;
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private float randomStartDelay = 2f;

    [Header("Choose Animation Type (Check ONE)")]
    [SerializeField] private bool useBreathing = true;
    [SerializeField] private bool useHovering = false;
    [SerializeField] private bool useSwaying = false;
    [SerializeField] private bool usePulsing = false;

    [Header("Hover Settings")]
    [SerializeField] private float hoverDistance = 0.2f;

    [Header("Sway Settings")]
    [SerializeField] private float swayAngle = 5f;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Vector3 originalRotation;
    private Sequence animationSequence;

    private void Start()
    {
        // Store original values
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
        originalRotation = transform.localEulerAngles;

        if (playOnStart)
        {
            // Random delay so everything doesn't animate in sync
            float delay = Random.Range(0f, randomStartDelay);
            DOVirtual.DelayedCall(delay, StartAnimation);
        }
    }

    public void StartAnimation()
    {
        StopAnimation();

        if (useBreathing)
            StartBreathingAnimation();
        else if (useHovering)
            StartHoverAnimation();
        else if (useSwaying)
            StartSwayAnimation();
        else if (usePulsing)
            StartPulseAnimation();
        else
            StartBreathingAnimation(); // Default to breathing
    }

    public void StopAnimation()
    {
        if (animationSequence != null)
        {
            animationSequence.Kill();
            animationSequence = null;
        }

        // Reset to original values
        transform.localScale = originalScale;
        transform.localPosition = originalPosition;
        transform.localEulerAngles = originalRotation; 
    }

    private void StartBreathingAnimation()
    {
        Vector3 breatheScale = originalScale * (1f + breathingStrength);
        
        animationSequence = DOTween.Sequence();
        animationSequence
            .Append(transform.DOScale(breatheScale, breathingDuration / 2)
                .SetEase(Ease.InOutSine))
            .Append(transform.DOScale(originalScale, breathingDuration / 2)
                .SetEase(Ease.InOutSine))
            .SetLoops(-1);
    }

    private void StartHoverAnimation()
    {
        Vector3 hoverUp = originalPosition + Vector3.up * hoverDistance;
        
        animationSequence = DOTween.Sequence();
        animationSequence
            .Append(transform.DOLocalMove(hoverUp, breathingDuration / 2)
                .SetEase(Ease.InOutSine))
            .Append(transform.DOLocalMove(originalPosition, breathingDuration / 2)
                .SetEase(Ease.InOutSine))
            .SetLoops(-1);
    }

    private void StartSwayAnimation()
    {
        Vector3 swayLeft = originalRotation + Vector3.forward * swayAngle;
        Vector3 swayRight = originalRotation + Vector3.forward * -swayAngle;
        
        animationSequence = DOTween.Sequence();
        animationSequence
            .Append(transform.DOLocalRotate(swayLeft, breathingDuration / 2)
                .SetEase(Ease.InOutSine))
            .Append(transform.DOLocalRotate(swayRight, breathingDuration / 2)
                .SetEase(Ease.InOutSine))
            .Append(transform.DOLocalRotate(originalRotation, breathingDuration / 2)
                .SetEase(Ease.InOutSine))
            .SetLoops(-1);
    }

    private void StartPulseAnimation()
    {
        Vector3 pulseScale = originalScale * (1f + breathingStrength * 2);
        
        animationSequence = DOTween.Sequence();
        animationSequence
            .Append(transform.DOScale(pulseScale, 0.1f)
                .SetEase(Ease.OutBack))
            .Append(transform.DOScale(originalScale, 0.3f)
                .SetEase(Ease.InBack))
            .AppendInterval(breathingDuration - 0.4f)
            .SetLoops(-1);
    }

    private void OnDestroy()
    {
        if (animationSequence != null)
            animationSequence.Kill();
    }

    private void OnDisable()
    {
        StopAnimation();
    }

    // Quick test buttons for the Inspector
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [ContextMenu("Test Breathing")]
    public void TestBreathing()
    {
        useBreathing = true;
        useHovering = false;
        useSwaying = false;
        usePulsing = false;
        StartAnimation();
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [ContextMenu("Test Hovering")]
    public void TestHovering()
    {
        useBreathing = false;
        useHovering = true;
        useSwaying = false;
        usePulsing = false;
        StartAnimation();
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [ContextMenu("Test Swaying")]
    public void TestSwaying()
    {
        useBreathing = false;
        useHovering = false;
        useSwaying = true;
        usePulsing = false;
        StartAnimation();
    }
}