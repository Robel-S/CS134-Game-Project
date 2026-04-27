using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Transform cameraPosition;

    // Update is called once per frame
    void Update()
    {
        //changes camera position to the palyers cameraPosition child object
        transform.position = cameraPosition.position;
    }
}
