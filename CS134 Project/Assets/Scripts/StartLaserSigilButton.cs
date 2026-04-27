using UnityEngine;

public class StartLaserSigilButton : MonoBehaviour
{
    public LaserSigilPuzzleManager puzzleManager;

    public Renderer buttonRenderer;
    public Material offMaterial;
    public Material onMaterial;

    public bool hasStarted = false;

    public void Activate()
    {
        if (hasStarted) return;

        hasStarted = true;

        if (buttonRenderer != null && onMaterial != null)
        {
            buttonRenderer.material = onMaterial;
        }

        GetComponent<AudioSource>().Play();

        if (puzzleManager != null)
        {
            puzzleManager.StartPuzzle();
        }

        Debug.Log("StartLaserSigilButton pressed.");
    }
}

/*

using UnityEngine;

public class StartLaserSigilButton : MonoBehaviour
{
    public bool isActivated = false;

    public Renderer buttonRenderer;
    public Material offMaterial;
    public Material onMaterial;

    public LaserRotator[] targetLasers;

    void Start()
    {
        UpdateVisual();
    }

    public void Activate()
    {
        // Toggle button state
        isActivated = !isActivated;

        // Change material based on state
        UpdateVisual();

        GetComponent<AudioSource>().Play();

        // Toggle laser rotation
        foreach (LaserRotator laser in targetLasers)
        {
            if (laser != null)
            {
                if (laser.isRotating)
                    laser.StopRotation();
                else
                    laser.StartRotation();
            }
        }

        Debug.Log(gameObject.name + " toggled all lasers!");
    }

    void UpdateVisual()
    {
        if (buttonRenderer == null) return;

        if (isActivated && onMaterial != null)
        {
            buttonRenderer.material = onMaterial;
        }
        else if (!isActivated && offMaterial != null)
        {
            buttonRenderer.material = offMaterial;
        }
    }
}

 */