using UnityEngine;

public class LaserRotator : MonoBehaviour
{
    // Speed of rotation (degrees per second)
    public float rotationSpeed = 10f;

    // Controls whether the laser is currently rotating
    public bool isRotating = false;

    // Axis the object rotates around (set in Inspector)
    [Header("Axis to rotate the parent LaserPoint around")]
    public Vector3 rotationAxis = Vector3.right;

    void Update()
    {
        // Only rotate if enabled
        if (isRotating)
        {
            // Rotate object smoothly over time in local space
            transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    // Start rotating the laser
    public void StartRotation()
    {
        isRotating = true;
    }

    // Stop rotating the laser
    public void StopRotation()
    {
        isRotating = false;
    }

    // Toggle rotation on/off
    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }
}
