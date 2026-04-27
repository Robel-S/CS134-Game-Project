using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwap : MonoBehaviour
{
    private bool isTouchingSurface = false;
    [SerializeField] Animator camAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G) && isTouchingSurface)
        {
            Physics.gravity = -Physics.gravity;
            camAnimator.SetTrigger("G_Pressed");
            GetComponent<AudioSource>().Play();
        }
    }
    void OnCollisionStay(Collision collision)
    {
        isTouchingSurface = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isTouchingSurface = false;
    }
}
