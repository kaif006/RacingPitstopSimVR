using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables; 

public class WheelNut : MonoBehaviour, IDrillable 
{
    [Header("Dependencies")]
    public XRGrabInteractable wheelInteractable;
    
    [Header("Settings")]
    public float unscrewTimeRequired = 0.8f; 
    
    private float currentTimer = 0f;
    private bool isUnscrewed = false;

    public void ProgressDrilling(float deltaTime)
    {
        if (isUnscrewed) return;

        currentTimer += deltaTime;

        if (currentTimer >= unscrewTimeRequired)
        {
            UnlockWheel();
        }
    }

    void UnlockWheel()
    {
        isUnscrewed = true;
        wheelInteractable.interactionLayers = InteractionLayerMask.GetMask("Default");
        // pop wheel out (x-axis)
        wheelInteractable.transform.localPosition += Vector3.right * 0.05f;
    }

    public void ResetNut()
    {
        isUnscrewed = false;
        currentTimer = 0;
        wheelInteractable.interactionLayers = InteractionLayerMask.GetMask("LockedWheel");
    }
}