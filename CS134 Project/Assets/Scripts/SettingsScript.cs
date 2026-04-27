using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public GameObject settingsMenu;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if user presses escape settings button dissapears and setting menu appears and cursor becomes unlocked and visible
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);    
            settingsMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
