using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(AudioSource))]
public class DrillTrigger : MonoBehaviour
{
    [Header("Drill Settings")]
    public GameObject drillBit;
    public float spinSpeed = 1500f;
    public Vector3 rotationAxis = Vector3.forward;

    [Header("Audio Settings")]
    public AudioClip drillAudioClip;
    private AudioSource drillAudioSource;

    [Header("Haptic Settings")]
    [Range(0f, 1f)] public float hapticIntensity = 0.5f;

    private bool isSpinning = false;
    
    private XRBaseInputInteractor activeInteractor; 

    void Start()
    {
        drillAudioSource = GetComponent<AudioSource>();
        
        if (drillAudioClip != null)
        {
            drillAudioSource.clip = drillAudioClip;
            drillAudioSource.loop = true;
            drillAudioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        if (isSpinning)
        {
            if (drillBit != null)
            {
                drillBit.transform.Rotate(rotationAxis * spinSpeed * Time.deltaTime);
            }

            if (activeInteractor != null)
            {
                activeInteractor.SendHapticImpulse(hapticIntensity, Time.deltaTime); 
            }
        }
    }

    public void StartSpinning(ActivateEventArgs args)
    {
        isSpinning = true;

        if (args.interactorObject is XRBaseInputInteractor inputInteractor)
        {
            activeInteractor = inputInteractor;
        }
        
        if (drillAudioSource != null && !drillAudioSource.isPlaying)
        {
            drillAudioSource.Play();
        }
    }

    public void StopSpinning(DeactivateEventArgs args)
    {
        isSpinning = false;
        activeInteractor = null;
        
        if (drillAudioSource != null && drillAudioSource.isPlaying)
        {
            drillAudioSource.Stop();
        }
    }
}