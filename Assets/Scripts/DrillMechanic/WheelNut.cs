using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class WheelNut : MonoBehaviour, IDrillable 
{
    [Header("Dependencies")]
    public XRGrabInteractable wheelInteractable;
    
    [Header("Settings")]
    public float unscrewTimeRequired = 0.8f; 
    
    // --- Define the Events ---
    public delegate void WheelAction();
    public event WheelAction OnWheelUnlocked;
    public event WheelAction OnWheelLocked;

    private float currentTimer = 0f;
    public bool isLocked = true;

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

        if (isLocked)
        {
            currentTimer += deltaTime;
            if (currentTimer >= unscrewTimeRequired)
            {
                UnlockWheel();
            }
        }
        else if (isSocketed)
        {
            currentTimer += deltaTime;
            if (currentTimer >= unscrewTimeRequired)
            {
                LockWheel();
            }
        }
    }

    void UnlockWheel()
    {
        isLocked = false;
        currentTimer = 0;
        
        wheelInteractable.interactionLayers = InteractionLayerMask.GetMask("Default");
        OnWheelUnlocked?.Invoke();
    }

    void LockWheel()
    {
        isLocked = true;
        currentTimer = 0;
        
        wheelInteractable.interactionLayers = InteractionLayerMask.GetMask("LockedWheel");
        OnWheelLocked?.Invoke();
    }

    public void ResetNut()
    {
        isLocked = true;
        currentTimer = 0;
        wheelInteractable.interactionLayers = InteractionLayerMask.GetMask("LockedWheel");
    }
}