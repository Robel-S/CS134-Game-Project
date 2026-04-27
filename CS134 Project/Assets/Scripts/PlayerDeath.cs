using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;

    // Start is called before the first frame update
    void Start()
    {
        //gets player position and rotation at the start of the game
        startPos = transform.position;
        startRot = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            //if player collides with a lazer sends them to the starting position and rotation
            transform.position = startPos;
            transform.rotation = startRot;

            //stops players momentum on respwan 
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
