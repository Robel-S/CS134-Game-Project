using UnityEngine;

[ExecuteAlways]
public class LaserGroupRotationController : MonoBehaviour
{
    [Header("Drag LaserPoint parent objects here")]
    public Transform[] laserPoints;

    [Header("Uniform Rotation Offset")]
    [Range(-180f, 180f)] public float xOffset = 0f;
    [Range(-180f, 180f)] public float yOffset = 0f;
    [Range(-180f, 180f)] public float zOffset = 0f;

    [Header("Controls")]
    public bool enablePreview = false;
    public bool captureStartingRotations = false;
    public bool applyOffsetsNow = false;
    public bool resetOffsets = false;

    private Quaternion[] startingRotations;

    void OnValidate()
    {
        if (captureStartingRotations)
        {
            captureStartingRotations = false;
            CaptureStartingRotations();
        }

        if (resetOffsets)
        {
            resetOffsets = false;
            xOffset = 0f;
            yOffset = 0f;
            zOffset = 0f;
        }

        if (applyOffsetsNow)
        {
            applyOffsetsNow = false;
            ApplyOffsets();
        }

        if (enablePreview)
        {
            ApplyOffsets();
        }
    }

    void CaptureStartingRotations()
    {
        if (laserPoints == null) return;

        startingRotations = new Quaternion[laserPoints.Length];

        for (int i = 0; i < laserPoints.Length; i++)
        {
            if (laserPoints[i] != null)
            {
                startingRotations[i] = laserPoints[i].localRotation;
            }
        }

        Debug.Log("Captured starting laser rotations.");
    }

    void ApplyOffsets()
    {
        if (laserPoints == null) return;

        if (startingRotations == null || startingRotations.Length != laserPoints.Length)
        {
            Debug.LogWarning("Starting rotations not captured yet. Check 'Capture Starting Rotations' first.");
            return;
        }

        Quaternion offsetRotation = Quaternion.Euler(xOffset, yOffset, zOffset);

        for (int i = 0; i < laserPoints.Length; i++)
        {
            if (laserPoints[i] == null) continue;

            // Uniform group offset while preserving each laser's captured starting rotation
            laserPoints[i].localRotation = offsetRotation * startingRotations[i];
        }
    }
}