using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonsPressed : MonoBehaviour
{
    public InteractableButton[] buttons;
    private bool hasWon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasWon) return;

        int count = 0;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].isActivated)
            {
                count++;
            }
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = "Buttons Pressed: " + count + "/" + buttons.Length;
        if (count == buttons.Length) Won();
    }

    private void Won()
    {
        hasWon = true;
        gameObject.GetComponent<TextMeshProUGUI>().text = "MISSION ACCOMPLISHED";
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = Vector2.zero;
        gameObject.GetComponent<TextMeshProUGUI>().fontSize = 28;
        gameObject.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;

    }
}
