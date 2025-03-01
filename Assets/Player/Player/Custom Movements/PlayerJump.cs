using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] public float jumpVelocity = 8f;
    [SerializeField] public float fallMultiplier = 1.5f; // Multiplier for falling
    [SerializeField] public float lowJumpMultiplier = 2f; // Multiplier for low jumps
    Rigidbody rb;
    bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    bool IsImobilized;
    void Update()
    {
        if (!IsImobilized)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                // Add upward velocity to the Rigidbody
                rb.velocity += Vector3.up * jumpVelocity;

                isGrounded = false; // Player is no longer grounded
            }
        }
        
    }
    internal void DisableJump()
    {
        IsImobilized = true;
    }
    internal void EnableJump()
    {
        IsImobilized = false;
    }
    void FixedUpdate()
    {
        // Apply gravity with modified multipliers
        if (rb.velocity.y < 0) // If falling
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        /*else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) short jump
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }*/
    }
}