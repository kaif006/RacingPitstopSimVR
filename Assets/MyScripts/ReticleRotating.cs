using UnityEngine;

public class ReticleAnimator : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 90f; // Degrees per second

    [Header("Scale (Pulse) Settings")]
    public float pulseSpeed = 2f;
    public float minScale = 0.9f;
    public float maxScale = 1.1f;

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        RotateReticle();
        PulseReticle();
    }

    void RotateReticle()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }

    void PulseReticle()
    {
        float scaleFactor = Mathf.Lerp(
            minScale,
            maxScale,
            (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f
        );

        transform.localScale = initialScale * scaleFactor;
    }
}
