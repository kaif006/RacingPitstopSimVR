using UnityEngine;

[RequireComponent(typeof(NewWheelNut))]
[RequireComponent(typeof(AudioSource))]
public class NewWheelSyncOutline : MonoBehaviour
{
    [Header("Dependencies")]
    private NewWheelNut nut;
    private AudioSource audioSource;

    [Header("Target Renderers")]
    public Renderer[] outlineRenderers; 

    [Header("Feedback Settings")]
    [ColorUsage(true, true)] public Color lockedColor = Color.red;
    [ColorUsage(true, true)] public Color unlockedColor = Color.green;
    
    public ParticleSystem lockSparks;
    public AudioClip lockSound;

    private int outlineColorId;
    private MaterialPropertyBlock propertyBlock;

    private void Awake()
    {
        nut = GetComponent<NewWheelNut>();
        audioSource = GetComponent<AudioSource>();
        propertyBlock = new MaterialPropertyBlock();
        outlineColorId = Shader.PropertyToID("_outlineColor"); 
    }

    private void Start()
    {
        nut.OnWheelUnlocked += OnNutUnlocked;
        nut.OnWheelLocked += OnNutLocked;
        
        // Ensure we start in the correct visual state
        InitialState();
    }

    private void OnDestroy()
    {
        if (nut != null) 
        {
            nut.OnWheelUnlocked -= OnNutUnlocked;
            nut.OnWheelLocked -= OnNutLocked;
        }
    }

    private void InitialState()
    {
        // Start popped out and Green
        transform.localPosition = new Vector3(0.05f, 0, 0); 
        ApplyOutlineColor(unlockedColor);
    }

    // private void OnNutUnlocked()
    // {
    //     transform.localPosition = new Vector3(0.05f, 0, 0);
    //     ApplyOutlineColor(unlockedColor);
    //     TriggerVFX(); 
    // }

    // private void OnNutLocked()
    // {
    //     // Flush with the wheel and Red
    //     transform.localPosition = Vector3.zero;
    //     ApplyOutlineColor(lockedColor);
    //     TriggerVFX();
    // }
    private void OnNutUnlocked()
{
    // Enable physics so the tire can fall/be moved
    Rigidbody rb = GetComponent<Rigidbody>();
    if (rb != null) rb.isKinematic = false; 

    ApplyOutlineColor(unlockedColor);
}

private void OnNutLocked()
{
    // Disable physics so the tire stays perfectly stuck to the car
    Rigidbody rb = GetComponent<Rigidbody>();
    if (rb != null) rb.isKinematic = true; 

    ApplyOutlineColor(lockedColor);
    TriggerVFX();
}

    private void TriggerVFX()
    {
        if (lockSparks != null) lockSparks.Play();
        if (lockSound != null) audioSource.PlayOneShot(lockSound);
    }

    private void ApplyOutlineColor(Color color)
    {
        propertyBlock.SetColor(outlineColorId, color);
        foreach (var renderer in outlineRenderers)
        {
            if (renderer != null) renderer.SetPropertyBlock(propertyBlock);
        }
    }
}