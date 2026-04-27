using UnityEngine;

// Allows manual rotation adjustment of multiple laser points in the Editor
[ExecuteAlways]
public class LaserGroupRotationController : MonoBehaviour
{
    // LaserPoint parent objects to be rotated together
    [Header("Drag LaserPoint parent objects here")]
    public Transform[] laserPoints;

    // Offset rotation applied uniformly to all lasers
    [Header("Uniform Rotation Offset")]
    [Range(-180f, 180f)] public float xOffset = 0f;
    [Range(-180f, 180f)] public float yOffset = 0f;
    [Range(-180f, 180f)] public float zOffset = 0f;

    // Editor control toggles
    [Header("Controls")]
    public bool enablePreview = false;               // Continuously apply rotation in editor
    public bool captureStartingRotations = false;    // Save initial rotations
    public bool applyOffsetsNow = false;             // Apply offsets once
    public bool resetOffsets = false;                // Reset offsets to zero

    // Stores original rotations of each laser point
    private Quaternion[] startingRotations;

    void OnValidate()
    {
        // Capture initial rotations when toggled
        if (captureStartingRotations)
        {
            captureStartingRotations = false;
            CaptureStartingRotations();
        }

        // Reset offsets to default values
        if (resetOffsets)
        {
            resetOffsets = false;
            xOffset = 0f;
            yOffset = 0f;
            zOffset = 0f;
        }

        // Apply offsets once
        if (applyOffsetsNow)
        {
            applyOffsetsNow = false;
            ApplyOffsets();
        }

        // Continuously update in editor if preview is enabled
        if (enablePreview)
        {
            ApplyOffsets();
        }
    }

    // Saves the initial local rotations of each laser point
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

    // Applies uniform rotation offsets while preserving original rotations
    void ApplyOffsets()
    {
        if (laserPoints == null) return;

        // Ensure starting rotations exist
        if (startingRotations == null || startingRotations.Length != laserPoints.Length)
        {
            Debug.LogWarning("Starting rotations not captured yet. Check 'Capture Starting Rotations' first.");
            return;
        }

        // Create rotation from offset values
        Quaternion offsetRotation = Quaternion.Euler(xOffset, yOffset, zOffset);

        for (int i = 0; i < laserPoints.Length; i++)
        {
            if (laserPoints[i] == null) continue;

            // Apply offset relative to original rotation
            laserPoints[i].localRotation = offsetRotation * startingRotations[i];
        }
    }
}
