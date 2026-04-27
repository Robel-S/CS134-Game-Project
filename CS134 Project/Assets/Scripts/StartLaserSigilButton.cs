using UnityEngine;

public class StartLaserSigilButton : MonoBehaviour
{
    // Reference to the puzzle manager that controls the laser puzzle
    public LaserSigilPuzzleManager puzzleManager;

    // Renderer used to visually change button state
    public Renderer buttonRenderer;

    // Materials for off/on states
    public Material offMaterial;
    public Material onMaterial;

    // Prevents button from being pressed multiple times
    public bool hasStarted = false;

    public void Activate()
    {
        // Ignore input if already activated
        if (hasStarted) return;

        hasStarted = true;

        // Change button appearance to "on"
        if (buttonRenderer != null && onMaterial != null)
        {
            buttonRenderer.material = onMaterial;
        }

        // Play button press sound
        GetComponent<AudioSource>().Play();

        // Start the puzzle logic
        if (puzzleManager != null)
        {
            puzzleManager.StartPuzzle();
        }

        Debug.Log("StartLaserSigilButton pressed.");
    }
}
