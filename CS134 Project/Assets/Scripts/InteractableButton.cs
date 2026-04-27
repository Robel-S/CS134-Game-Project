using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    public bool isActivated = false;
    public Renderer buttonRenderer;
    public Material offMaterial;
    public Material onMaterial;

    void Start()
    {
        if (buttonRenderer != null && offMaterial != null)
        {
            buttonRenderer.material = offMaterial;
        }
    }

    public void Activate()
    {
        if (isActivated) return;

        isActivated = true;

        if (buttonRenderer != null && onMaterial != null)
        {
            buttonRenderer.material = onMaterial;
            GetComponent<AudioSource>().Play();
        }

        Debug.Log(gameObject.name + " activated!");
    }
}