using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// This script manages all player movement including the reduction of stamina when sprinting
    /// </summary>
    public float rotationSensitivity = 2f; // Mouse rotation sensitivity
    public float rotationSmoothing = 0.1f; // Mouse rotation smoothing
    public float jumpAcceleration = 5f; // The acceleration that is applied to the player when they press space
    public float jumpGroundAllowance; // The variable distance above the ground allowed before the player can jump
    public Animator anim; // The player's animator object
    public Transform playerCamera; // The player's camera
    public GameObject player;
    public StatusBarScript statusBarScript; // To allow the changing of stamina
    public GameObject staminaBar;
    public bool allowPlayerMovement; // Allows the player to be stopped when they die or other

    private float speed = 5f; // Player movement speed
    private bool isSprinting, isRefilling;
    private Rigidbody rb;
    private CapsuleCollider playerCollider;
    private Vector3 movementInput; // The vector that the player is moved by
    private RaycastHit hit; // The ray object that holds that it hit and how far away it is
    public float rotationSpeed = 100f; // Player rotation speed

    private float yRotation;
    private float xRotation;
    public float sensX;
    public float sensY;

    private float distToGround; // The given distance from the ground
    private double desiredTimeAtJump = 0; // The time after the cooldown has elapsed for the player to jump

    private void Start()
    {
        // Sets defaults and the basic required values for the variables
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        distToGround = playerCollider.bounds.extents.y;

        StatusBarScript statusBarScript = player.GetComponent<StatusBarScript>();
        allowPlayerMovement = true;
        sensX = 120;
        sensY = 120;
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    private void Update()
    {
        if (allowPlayerMovement)
        {
            // If the player is allowed to move receive input
            // Get player input
            movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            // Player movement
            // Get Mouse Input
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
            // Apply Mouse Input
            yRotation += mouseX;
            xRotation -= mouseY;
            // Clamp Input to 90 degrees
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            // Rotate camera and player
            playerCamera.rotation = Quaternion.Euler(xRotation, 0, 0);
            rb.MoveRotation(Quaternion.Euler(new Vector3(0, yRotation, 0)));

            // Moves the rigid body
            rb.MovePosition(rb.position + transform.TransformDirection(movementInput) * speed * Time.deltaTime);

            // Sets the camera's x rotation which is independant to the player's rotation unlike the y rotation
            playerCamera.transform.rotation = Quaternion.Euler(new Vector3(xRotation, yRotation, 0));
            

            // This function returns a distance from the bottom of the player to an object
            Physics.Raycast(transform.position, -Vector3.up, out hit);
            float distancetoGround = hit.distance - 1;

            if (Input.GetKey(KeyCode.Space) && distancetoGround <= jumpGroundAllowance && desiredTimeAtJump <= Time.fixedTimeAsDouble)
            {
                // If the player presses space, the distance to the ground is within limits and the cooldown has elapsed. Jump
                desiredTimeAtJump = Time.fixedTimeAsDouble + 0.1;
                rb.velocity += new Vector3(0f, jumpAcceleration, 0f);
            }

            // Player Sprint
            if (statusBarScript.playerStamina > 1)
            {
                // If the stamina is greater than 1, this stops the persistent sprint bug 
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    // If the player holds the sprint key try to sprint
                    if (!isSprinting && statusBarScript.playerStamina > 0 && isRefilling == false)
                    {
                        // If the player is not already sprinting, the stamina is greater than 0 and the stamina bar is not refilling 
                        isSprinting = true;
                        speed = 10f; // Increase the speed
                        StartCoroutine(sprintBarDecrease()); // Start decreasing the sprint
                    }
                }
                else
                {
                    if (isSprinting)
                    {
                        // If the player is sprinting and the sprint key is not held down
                        isSprinting = false; // Stop the player from sprinting 
                        isRefilling = true; 
                        speed = 5f; // Slow the player back down to normal
                        StartCoroutine(sprintBarIncrease()); // Start refilling the stamina bar
                    }
                }
            }
            else
            {
                if (isSprinting)
                {
                    // If the stamina is less than 1 and the player is sprinting
                    isSprinting = false; // Stop the player sprinting
                    isRefilling = true;
                    speed = 5f; // Slow the player down
                    StartCoroutine(sprintBarIncrease()); // Start increasing the stamina
                }
            }
            if (statusBarScript.playerStamina >= 10 && isRefilling)
            {
                // If the stamina is 10 or more the player is now able to sprint again
                isRefilling = false;
                staminaBar.GetComponent<Image>().color = new Color32(60, 147, 245, 255); // Sets stamina bar back to blue when player can sprint again
            }
            else if (isRefilling)
            {
                // If the stamina is less than 10 set the stamina bar to red to notify the player that it isn't usuable
                staminaBar.GetComponent<Image>().color = new Color32(245, 49, 49, 255); // Sets stamina bar to red when the bar is not usable/refilling
            }

            // Get input values
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Set the animator values
            anim.SetFloat("Horizontal", horizontalInput);
            anim.SetFloat("Vertical", verticalInput);
        }
    }

    // Coroutine for sprint decrease
    IEnumerator sprintBarDecrease()
    {
        while (statusBarScript.playerStamina > 0 && isSprinting)
        {
            // If the player's stamina is greater than 0 and they are sprinting, start decreasing the stamina
            yield return new WaitForSecondsRealtime(0.01f); // Waits for 0.01 seconds then decreases stamina by 0.1
            statusBarScript.changePlayerStamina(-0.1f);
        }
    }

    // Coroutine for sprint increase
    IEnumerator sprintBarIncrease()
    {
        while (statusBarScript.playerStamina < 100 && !isSprinting)
        {
            // If the player stamina is less than 100 and the player is not sprinting, start increasing the stamina
            statusBarScript.changePlayerStamina(0.25f); // Increases stamina by 0.25 every 0.1 seconds
            yield return new WaitForSecondsRealtime(0.1f);
        }
        yield break;
    }
}