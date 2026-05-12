using UnityEngine;

public class PhysicalButton : MonoBehaviour
{
    [Header("Manager Reference")]
    public PitManager pitManager;

    [Header("Feedback (Optional)")]
    public AudioSource slapSound;

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponentInParent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor>() != null)
        {
            Debug.Log("VR Hand Physically smacked the button!");
            
            if (pitManager != null)
            {
                pitManager.AttemptRelease();
                Debug.Log("Button Pressed");
            }

            if (slapSound != null)
            {
                slapSound.Play();
            }
        }
    }
}