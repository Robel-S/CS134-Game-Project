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
        //checks if g key is pressed and if the player is touching the surface and if so it switches the player gravity
        if (Input.GetKeyDown(KeyCode.G) && isTouchingSurface)
        {
            Physics.gravity = -Physics.gravity;
            //trigger cam animation
            camAnimator.SetTrigger("G_Pressed");
            //trigger gravity switching sound
            GetComponent<AudioSource>().Play();
        }
    }
    //if user collides with something set isTouchingSurface to True
    void OnCollisionStay(Collision collision)
    {
        isTouchingSurface = true;
    }
    //if user stops colliding with stuff set isTouchingSurface to false
    void OnCollisionExit(Collision collision)
    {
        isTouchingSurface = false;
    }
}
