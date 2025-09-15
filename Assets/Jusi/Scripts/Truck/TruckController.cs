using UnityEngine;
using DG.Tweening;
using System;

public class TruckController : MonoBehaviour
{
    [Header("Truck Settings")]
    [SerializeField] private Transform truckStart;   // Garage / off-screen start position
    [SerializeField] private float driveDuration = 2f;
    [SerializeField] private ParticleSystem exhaustSmoke;
    [SerializeField] private bool debugMode = true; // Add this for debugging

    private bool isMoving = false;
    private Vector3 originalPosition;

    private void Start()
    {
        // Store original position and place the truck at its start position initially
        if (truckStart != null)
        {
            originalPosition = truckStart.position;
            transform.position = truckStart.position;
            
            if (debugMode)
                Debug.Log($" Truck initialized at position: {transform.position}");
        }
        else
        {
            // If no start position is set, use current position
            originalPosition = transform.position;
            Debug.LogWarning(" Truck Start position not set! Using current position as start.");
        }
    }

    /// <summary>
    /// Drives the truck from start to a stall.
    /// </summary>
    public void DriveToStall(Transform stallTarget, Action onArrive = null)
    {
        if (stallTarget == null)
        {
            Debug.LogError(" Stall target is null! Cannot drive truck.");
            return;
        }

        if (isMoving)
        {
            if (debugMode)
                Debug.Log(" Truck is already moving, ignoring drive request.");
            return;
        }

        isMoving = true;

        if (debugMode)
            Debug.Log($" Driving truck from {transform.position} to {stallTarget.position}");

        // Start exhaust smoke
        if (exhaustSmoke != null) 
            exhaustSmoke.Play();

        // Kill any existing tweens on this transform
        transform.DOKill();

        // Drive to stall
        transform.DOMove(stallTarget.position, driveDuration)
            .SetEase(Ease.InOutQuad) // Changed from Linear for smoother movement
            .OnComplete(() =>
            {
                isMoving = false;
                
                // Stop exhaust smoke
                if (exhaustSmoke != null) 
                    exhaustSmoke.Stop();

                if (debugMode)
                    Debug.Log(" Truck arrived at stall!");

                // Small bounce effect when truck stops
                transform.DOShakePosition(0.3f, 0.1f, 5, 90, false, true)
                    .SetDelay(0.1f);

                // Notify that truck has arrived
                onArrive?.Invoke();
            });
    }

    /// <summary>
    /// Returns truck to its start/garage position.
    /// </summary>
    public void ReturnToStart(Action onReturn = null)
    {
        Vector3 targetPosition = truckStart != null ? truckStart.position : originalPosition;

        if (isMoving)
        {
            if (debugMode)
                Debug.Log(" Truck is already moving, ignoring return request.");
            return;
        }

        isMoving = true;

        if (debugMode)
            Debug.Log($"Returning truck from {transform.position} to {targetPosition}");

        // Start exhaust smoke
        if (exhaustSmoke != null) 
            exhaustSmoke.Play();

        // Kill any existing tweens on this transform
        transform.DOKill();

        // Return to start
        transform.DOMove(targetPosition, driveDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                isMoving = false;
                
                // Stop exhaust smoke
                if (exhaustSmoke != null) 
                    exhaustSmoke.Stop();

                if (debugMode)
                    Debug.Log(" Truck returned to start!");

                onReturn?.Invoke();
            });
    }

    /// <summary>
    /// Check if truck is currently moving
    /// </summary>
    public bool IsMoving()
    {
        return isMoving;
    }

    /// <summary>
    /// Force stop the truck (emergency stop)
    /// </summary>
    public void ForceStop()
    {
        if (debugMode)
            Debug.Log("Force stopping truck!");

        transform.DOKill();
        isMoving = false;
        
        if (exhaustSmoke != null) 
            exhaustSmoke.Stop();
    }

    private void OnDestroy()
    {
        // Clean up tweens when object is destroyed
        transform.DOKill();
    }
}