using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public InteractableButton[] buttons;
    public Vector3 openOffset = new Vector3(0, 4, 0);
    public float openSpeed = 2f;

    private Vector3 closedPosition;
    private Vector3 targetPosition;
    private bool shouldOpen = false;

    void Start()
    {
        closedPosition = transform.position;
        targetPosition = closedPosition;
    }

    void Update()
    {
        bool allActive = true;

        foreach (InteractableButton button in buttons)
        {
            if (button == null || !button.isActivated)
            {
                allActive = false;
                break;
            }
        }

        if (allActive)
        {
            shouldOpen = true;
            targetPosition = closedPosition + openOffset;
        }

        if (shouldOpen)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * openSpeed);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}