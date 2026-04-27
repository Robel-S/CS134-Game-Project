using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuScript : MonoBehaviour
{
    public GameObject settingsButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            settingsButton.SetActive(true);
            gameObject.SetActive(false);
            
        }
    }
    public void CloseSettings()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        settingsButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
