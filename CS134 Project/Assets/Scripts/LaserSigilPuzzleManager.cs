using UnityEngine;

public class LaserSigilPuzzleManager : MonoBehaviour
{
    // Reference to rotating laser object used for angle checking
    [Header("Main rotating laser object")]
    public Transform rotatingLaserTransform;

    // Controls laser rotation behavior
    public LaserRotator laserRotator;

    // Button that starts the puzzle
    public StartLaserSigilButton button;

    // Button that appears when puzzle is complete
    public InteractableButton finishButton;

    // Allowed error range when matching angles
    [Header("Target Settings")]
    public float toleranceDegrees = 5f;

    // Target angles representing different shapes
    public float[] targetZAngles =
    {
        0f,       // square
        -22.5f,   // star
        -45f,     // cross
        -67.5f,   // star
        -90f,     // square
        -112.5f,  // octagon
        -225f,    // rays
        -337.5f   // octagon
    };

    // Visual indicators on walls/floor
    [Header("Target Shape Objects On Wall")]
    public GameObject[] targetShapeObjects;

    // Materials for locked/unlocked states
    [Header("Materials")]
    public Material lockedMaterial;
    public Material unlockedMaterial;

    // Tracks puzzle state
    private bool puzzleStarted = false;
    private bool puzzleComplete = false;

    // Tracks which targets have been unlocked
    private bool[] unlockedTargets;

    void Start()
    {
        // Initialize all targets as locked
        unlockedTargets = new bool[targetZAngles.Length];

        // Set initial visual state
        UpdateTargetVisuals();
    }

    void Update()
    {
        // Do nothing if puzzle hasn't started or is finished
        if (!puzzleStarted || puzzleComplete) return;

        // Player presses E to attempt alignment
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckCurrentAlignment();
        }

        // Show finish button once puzzle is complete
        if (puzzleComplete)
        {
            finishButton.gameObject.SetActive(true);
        }
    }

    // Called when start button is pressed
    public void StartPuzzle()
    {
        puzzleStarted = true;
        Debug.Log("Laser sigil puzzle started.");
    }

    // Checks if current laser rotation matches any target angle
    void CheckCurrentAlignment()
    {
        if (rotatingLaserTransform == null) return;

        // Normalize rotation to 0–360 range
        float currentZ = NormalizeAngle(rotatingLaserTransform.localEulerAngles.z);

        Debug.Log("Checking current Z angle: " + currentZ);

        // Loop through all target angles
        for (int i = 0; i < targetZAngles.Length; i++)
        {
            float targetZ = NormalizeAngle(targetZAngles[i]);

            // Calculate smallest angle difference
            float difference = Mathf.Abs(Mathf.DeltaAngle(currentZ, targetZ));

            // If within tolerance, consider it a match
            if (difference <= toleranceDegrees)
            {
                // Skip if already unlocked
                if (unlockedTargets[i])
                {
                    Debug.Log("Target already matched.");
                }
                else
                {
                    // Handle grouped targets (same shape multiple angles)
                    if (i == 0 || i == 4)
                        unlockedTargets[0] = unlockedTargets[4] = true;
                    else if (i == 1 || i == 3)
                        unlockedTargets[1] = unlockedTargets[3] = true;
                    else if (i == 5 || i == 7)
                        unlockedTargets[5] = unlockedTargets[7] = true;
                    else
                        unlockedTargets[i] = true;

                    Debug.Log("Unlocked target " + targetZAngles[i] + " at angle " + currentZ);

                    // Update visuals and check for completion
                    UpdateTargetVisuals();
                    CheckPuzzleComplete();
                }

                return;
            }
        }

        // No matches found
        Debug.Log("No target matched at angle " + currentZ);
    }

    // Checks if all targets have been unlocked
    void CheckPuzzleComplete()
    {
        for (int i = 0; i < unlockedTargets.Length; i++)
        {
            if (!unlockedTargets[i])
            {
                return;
            }
        }

        puzzleComplete = true;

        Debug.Log("LASER SIGIL PUZZLE COMPLETE!");
    }

    // Updates visual state of target indicators
    void UpdateTargetVisuals()
    {
        if (targetShapeObjects == null) return;

        for (int i = 0; i < targetShapeObjects.Length; i++)
        {
            if (targetShapeObjects[i] == null) continue;

            Renderer r = targetShapeObjects[i].GetComponent<Renderer>();
            if (r == null) continue;

            // Apply correct material based on unlock state
            if (i < unlockedTargets.Length && unlockedTargets[i])
            {
                if (unlockedMaterial != null)
                    r.material = unlockedMaterial;
            }
            else
            {
                if (lockedMaterial != null)
                    r.material = lockedMaterial;
            }
        }
    }

    // Converts angle to range [0, 360]
    float NormalizeAngle(float angle)
    {
        angle %= 360f;

        if (angle < 0f)
        {
            angle += 360f;
        }

        return angle;
    }
}
