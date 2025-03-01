using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float crouchSpeed = 8f; // Speed of smooth crouch transition
    public float crouchDepth = 0.4f; // 0.4 means 40% of normal height

    private CharacterController controller;
    private Vector3 velocity;
    private float initialHeight;
    private float crouchHeight;
    private Vector3 initialCenter;
    private bool isCrouching = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        initialHeight = controller.height;
        crouchHeight = initialHeight * crouchDepth;
        initialCenter = controller.center;
    }

    void Update()
    {
        // Check if the player is grounded
        bool isGrounded = controller.isGrounded;

        // If grounded, reset the downward velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Movement speed
        float speed = isCrouching ? walkSpeed * 0.6f : (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // **Crouching Logic**
        bool wantsToCrouch = Input.GetKey(KeyCode.LeftControl);
        bool canStand = !Physics.Raycast(transform.position, Vector3.up, initialHeight * 0.75f);

        if (wantsToCrouch || (!wantsToCrouch && !canStand))
            isCrouching = true;
        else if (!wantsToCrouch && canStand)
            isCrouching = false;

        float targetHeight = isCrouching ? crouchHeight : initialHeight;
        controller.height = Mathf.Lerp(controller.height, targetHeight, Time.deltaTime * crouchSpeed);
        controller.center = Vector3.Lerp(controller.center, new Vector3(initialCenter.x, isCrouching ? initialCenter.y - (initialHeight - crouchHeight) / 2 : initialCenter.y, initialCenter.z), Time.deltaTime * crouchSpeed);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
