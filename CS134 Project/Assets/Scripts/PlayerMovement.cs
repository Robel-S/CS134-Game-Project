using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float playerHeight;
    public float groundDrag;
    public Transform orientation;
    public LayerMask isGround;
    float horizontalInput;
    float verticalInput;
    bool grounded;
    
    Vector3 moveDirection;
    Rigidbody rb;

    public AudioSource footSteps;
    public float stepInterval = 0.5f;
    private float stepTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);

        MyInput();
        SpeedControl();

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
        
        PlayFootsteps();
    }

    public void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);

        
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity
        if(flatVel.magnitude > speed)
        {
            Vector3 limited = flatVel.normalized * speed;
            rb.velocity = new Vector3(limited.x, rb.velocity.y, limited.z);
        }
    }

    private void PlayFootsteps()
    {
        // Check if player is moving
        bool isMoving = rb.velocity.magnitude > 0.1f;

        if (grounded && isMoving)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                footSteps.Play();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = stepInterval;
        }

    }
}
