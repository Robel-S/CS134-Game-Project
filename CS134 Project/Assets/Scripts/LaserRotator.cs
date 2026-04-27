using UnityEngine;

public class LaserRotator : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public bool isRotating = false;

    [Header("Axis to rotate the parent LaserPoint around")]
    public Vector3 rotationAxis = Vector3.right;

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    public void StartRotation()
    {
        isRotating = true;
    }

    public void StopRotation()
    {
        isRotating = false;
    }

    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }
}