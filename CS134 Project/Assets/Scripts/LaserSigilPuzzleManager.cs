using UnityEngine;

public class LaserSigilPuzzleManager : MonoBehaviour
{
    [Header("Main rotating laser object")]
    public Transform rotatingLaserTransform;
    public LaserRotator laserRotator;
    public StartLaserSigilButton button;
    public InteractableButton finishButton;

    [Header("Target Settings")]
    public float toleranceDegrees = 5f;

    public float[] targetZAngles =
    {
        0f,       // 0 square
        -22.5f,   // 1 star
        -45f,     // 2 cross
        -67.5f,   // 3 star
        -90f,     // 4 square
        -112.5f,  // 5 octagon
        -225f,    // 6 rays
        -337.5f   // 7 octagon
    };

    [Header("Target Shape Objects On Wall")]
    public GameObject[] targetShapeObjects;

    [Header("Materials")]
    public Material lockedMaterial;
    public Material unlockedMaterial;

    private bool puzzleStarted = false;
    private bool puzzleComplete = false;
    private bool[] unlockedTargets;

    void Start()
    {
        unlockedTargets = new bool[targetZAngles.Length];
        UpdateTargetVisuals();
    }

    void Update()
    {
        if (!puzzleStarted || puzzleComplete) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckCurrentAlignment();
        }

        if (puzzleComplete)
        {
            finishButton.gameObject.SetActive(true);
        }
    }

    public void StartPuzzle()
    {
        puzzleStarted = true;
        Debug.Log("Laser sigil puzzle started.");
    }

    void CheckCurrentAlignment()
    {
        if (rotatingLaserTransform == null) return;

        float currentZ = NormalizeAngle(rotatingLaserTransform.localEulerAngles.z);

        Debug.Log("Checking current Z angle: " + currentZ);

        for (int i = 0; i < targetZAngles.Length; i++)
        {
            float targetZ = NormalizeAngle(targetZAngles[i]);
            float difference = Mathf.Abs(Mathf.DeltaAngle(currentZ, targetZ));

            // if target match
            if (difference <= toleranceDegrees)
            {
                if (unlockedTargets[i]) // if already unlocked
                    Debug.Log("Target already matched.");
                else
                {
                    if (i == 0 || i == 4)
                        unlockedTargets[0] = unlockedTargets[4] = true;
                    else if (i == 1 || i == 3)
                        unlockedTargets[1] = unlockedTargets[3] = true;
                    else if (i == 5 || i == 7)
                        unlockedTargets[5] = unlockedTargets[7] = true;
                    else
                        unlockedTargets[i] = true;

                    Debug.Log("Unlocked target " + targetZAngles[i] + " at angle " + currentZ);

                    //UpdateTargetVisuals();
                    CheckPuzzleComplete();
                }
                return;
            }
        }
        Debug.Log("No target matched at angle " + currentZ);
    }

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

        //if (laserRotator != null)
        //{
        //    laserRotator.StopRotation();
        //}

        //button.hasStarted = false;
        Debug.Log("LASER SIGIL PUZZLE COMPLETE!");
    }

    void UpdateTargetVisuals()
    {
        if (targetShapeObjects == null) return;

        for (int i = 0; i < targetShapeObjects.Length; i++)
        {
            if (targetShapeObjects[i] == null) continue;

            Renderer r = targetShapeObjects[i].GetComponent<Renderer>();

            if (r == null) continue;

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