using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 4f;
    public Camera playerCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                InteractableButton normalButton = hit.collider.GetComponent<InteractableButton>();
                if (normalButton != null)
                {
                    normalButton.Activate();
                    return;
                }

                StartLaserSigilButton startButton = hit.collider.GetComponent<StartLaserSigilButton>();
                if (startButton != null)
                {
                    startButton.Activate();
                    return;
                }
            }
        }
    }
}