using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    // Tracks whether the button has already been pressed
    public bool isActivated = false;

    // Renderer used to visually change button state
    public Renderer buttonRenderer;

    // Materials for off/on states
    public Material offMaterial;
    public Material onMaterial;

    void Start()
    {
        // Set initial button appearance to "off"
        if (buttonRenderer != null && offMaterial != null)
        {
            buttonRenderer.material = offMaterial;
        }
    }

    // Called when player interacts with the button
    public void Activate()
    {
        // Prevent reactivation
        if (isActivated) return;

        isActivated = true;

        // Change appearance and play sound
        if (buttonRenderer != null && onMaterial != null)
        {
            buttonRenderer.material = onMaterial;

            // Play button press sound
            GetComponent<AudioSource>().Play();
        }

        Debug.Log(gameObject.name + " activated!");
    }
}
