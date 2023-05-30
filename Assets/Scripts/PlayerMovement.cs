using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Player movement speed
    public float rotationSensitivity = 2f; // Mouse rotation sensitivity
    public float rotationSmoothing = 0.1f; // Mouse rotation smoothing

    public Animator anim;
    private Rigidbody rb;
    private Vector3 movementInput;
    public float rotationSpeed = 100f; // Player rotation speed
    private float mouseXSmooth = 0f;
    private float mouseYSmooth = 0f;
    private float mouseXVel = 0f;
    private float mouseYVel = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get player input
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        float rawMouseX = Input.GetAxis("Mouse X");
        float rawMouseY = Input.GetAxis("Mouse Y");


        // Apply mouse smoothing
        mouseXSmooth = Mathf.SmoothDamp(mouseXSmooth, rawMouseX, ref mouseXVel, rotationSmoothing);
        mouseYSmooth = Mathf.SmoothDamp(mouseYSmooth, rawMouseY, ref mouseYVel, rotationSmoothing);

        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    private void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + transform.TransformDirection(movementInput) * speed * Time.fixedDeltaTime);

        // Rotate the player based on mouse input
        float xRotation = mouseXSmooth * rotationSpeed * Time.fixedDeltaTime;
        float yRotation = mouseYSmooth * rotationSpeed * Time.fixedDeltaTime;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * xRotation));
        rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.right * yRotation));

        // Get input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", horizontalInput);
        anim.SetFloat("Vertical", verticalInput);
    }
}
/*
public class PlayerMovement : MonoBehaviour
{
    public Animator anim;
    public Transform cam;

    public float rotationSpeed = 100f;
    public float moveSpeed = 5f;  // Speed at which the object moves

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", horizontalInput);
        anim.SetFloat("Vertical", verticalInput);

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = movementDirection.magnitude;
        magnitude = Mathf.Clamp01(magnitude);
        movementDirection = cam.forward * verticalInput + cam.right * horizontalInput;
        movementDirection.Normalize();

        rb.velocity = movementDirection * magnitude * moveSpeed;

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, toRotation, rotationSpeed * Time.deltaTime));
        }
    }
}*/