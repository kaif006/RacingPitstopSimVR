using UnityEngine;

[RequireComponent(typeof(WheelNut))]
[RequireComponent(typeof(AudioSource))]
public class WheelSyncOutline : MonoBehaviour
{
    [Header("Dependencies")]
    private WheelNut nut;
    private AudioSource audioSource;

    [Header("Target Renderers (Performance Optimized)")]
    [Tooltip("Drag ONLY the specific child objects that have the outline material here.")]
    public Renderer[] outlineRenderers; 

    [Header("Feedback Settings")]
    [ColorUsage(true, true)] public Color lockedColor = Color.red;
    [ColorUsage(true, true)] public Color unlockedColor = Color.green;
    
    public ParticleSystem unlockSparks;
    public AudioClip unlockSound;

    private int outlineColorId;
    private MaterialPropertyBlock propertyBlock;

    private void Awake()
    {
        nut = GetComponent<WheelNut>();
        audioSource = GetComponent<AudioSource>();

        propertyBlock = new MaterialPropertyBlock();
        
        outlineColorId = Shader.PropertyToID("_outlineColor"); 
    }

    private void Start()
    {
        nut.OnWheelUnlocked += OnNutUnlocked;
        ResetOutlines();
    }

    private void OnDestroy()
    {
        if (nut != null) nut.OnWheelUnlocked -= OnNutUnlocked;
    }

    private void OnNutUnlocked()
    {
        transform.localPosition += Vector3.right * 0.05f;
        
        ApplyOutlineColor(unlockedColor);
        
        if (unlockSparks != null) unlockSparks.Play();
        if (unlockSound != null) audioSource.PlayOneShot(unlockSound);
    }

    public void ResetOutlines()
    {
        nut.ResetNut();
        ApplyOutlineColor(lockedColor);
    }

    private void ApplyOutlineColor(Color color)
    {
        propertyBlock.SetColor(outlineColorId, color);

        foreach (var renderer in outlineRenderers)
        {
            if (renderer != null)
            {
                renderer.SetPropertyBlock(propertyBlock);
            }
        }
    }
}