using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class NewWheelNut : MonoBehaviour, IDrillable 
{
    [Header("Dependencies")]
    public XRGrabInteractable wheelInteractable;
    public Animator nutAnimator; 
    
    [Header("Settings")]
    public float timeToLock = 5.0f; // Updated to 5 seconds
    public string animationTriggerName = "isFitted"; 

    public delegate void WheelAction();
    public event WheelAction OnWheelUnlocked;
    public event WheelAction OnWheelLocked;

    private float currentTimer = 0f;
    public bool isLocked = false; // Start UNLOCKED

    public void ProgressDrilling(float deltaTime)
    {
        bool isSocketed = false;
        foreach (var interactor in wheelInteractable.interactorsSelecting)
        {
            if (interactor is XRSocketInteractor)
            {
                isSocketed = true;
                break;
            }
        }

        // Logic: If it's currently UNLOCKED and sitting in the socket, we drill to LOCK it
        if (!isLocked && isSocketed)
        {
            currentTimer += deltaTime;
            Debug.Log($"Locking progress: {currentTimer:F1} / {timeToLock}s");

            if (currentTimer >= timeToLock)
            {
                LockWheel();
            }
        }
        // Logic: If it's currently LOCKED, we drill to UNLOCK it
        else if (isLocked)
        {
            currentTimer += deltaTime;
            Debug.Log($"Unlocking progress: {currentTimer:F1} / {timeToLock}s");

            if (currentTimer >= timeToLock)
            {
                UnlockWheel();
            }
        }
    }

    void UnlockWheel()
    {
        isLocked = false;
        currentTimer = 0;
        // Allow the player to grab the wheel
        wheelInteractable.interactionLayers = InteractionLayerMask.GetMask("Default");
        OnWheelUnlocked?.Invoke();
        Debug.Log("Nut Unlocked - Wheel can be removed.");
    }

    void LockWheel()
    {
        isLocked = true;
        currentTimer = 0;
        // Prevent the player from grabbing the wheel
        wheelInteractable.interactionLayers = InteractionLayerMask.GetMask("LockedWheel");
        
        if (nutAnimator != null) nutAnimator.SetTrigger(animationTriggerName);
        
        OnWheelLocked?.Invoke();
        Debug.Log("Nut Locked - Wheel is fitted.");
    }

    public void ResetNut()
    {
        isLocked = false;
        currentTimer = 0;
        wheelInteractable.interactionLayers = InteractionLayerMask.GetMask("Default");
        OnWheelUnlocked?.Invoke();
    }
}