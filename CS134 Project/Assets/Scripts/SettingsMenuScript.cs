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
        //if the user presses escape the setting menu dissapears, button reappears and cursor goes back to being locked and invisble
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            settingsButton.SetActive(true);
            gameObject.SetActive(false);
            
        }
    }
    //if user presses the x button in the setting menu the setting menu dissapears and cursor goes back to being locked and invisible
    public void CloseSettings()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        settingsButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
