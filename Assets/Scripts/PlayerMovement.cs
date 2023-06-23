using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Player movement speed
    public float rotationSensitivity = 2f; // Mouse rotation sensitivity
    public float rotationSmoothing = 0.1f; // Mouse rotation smoothing
    public float jumpAcceleration = 5f;
    public float jumpGroundAllowance;
    public Animator anim;
    public Transform playerCamera;

    private Rigidbody rb;
    private CapsuleCollider playerCollider;
    private Vector3 movementInput;
    private RaycastHit hit;
    public float rotationSpeed = 100f; // Player rotation speed
    private float mouseXSmooth = 0f;
    private float mouseYSmooth = 0f;
    private float mouseXVel = 0f;
    private float mouseYVel = 0f;
    private float distToGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        distToGround = playerCollider.bounds.extents.y;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.1));
    }

private void Update()
    {
        // Get player input
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        float rawMouseX = Input.GetAxis("Mouse X");
        float rawMouseY = Input.GetAxis("Mouse Y");

        Physics.Raycast(transform.position, -Vector3.up, out hit);
        float distancetoGround = hit.distance - 1;
        if (Input.GetKeyDown(KeyCode.Space) && distancetoGround <= jumpGroundAllowance)
        {
            rb.velocity += new Vector3(0f, jumpAcceleration, 0f);
        }

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
        // Rotate the camera only on the X-axis (up and down)
        Vector3 cameraRotation = playerCamera.localEulerAngles;
        cameraRotation.x -= yRotation;
        playerCamera.localEulerAngles = cameraRotation;

        // Get input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", horizontalInput);
        anim.SetFloat("Vertical", verticalInput);
    }
}
