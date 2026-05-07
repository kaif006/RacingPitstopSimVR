using UnityEngine;

public class WheelRotate : MonoBehaviour
{
    public float rotationSpeed = 300f;

    void Update()
    {
        // Rotates wheel forward
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}