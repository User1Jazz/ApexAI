using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    // Based on Third Person Movement: https://www.youtube.com/watch?v=4HpC--2iowE

    // Required components
    public CharacterController playerController;
    public Transform cam;

    // Required parameters
    public float walkSpeed = 2.0f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Gravity and Jump
    public float gravity = -9.81f;
    private Vector3 velocity;

    // Ground check
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkSphereRadius = 0.4f;
    [SerializeField] private LayerMask groundMask;

    // Update is called once per frame
    void Update()
    {
        // Gravity
        Gravity();

        // Move
        Move();
    }

    // Method to move character
    void Move()
    {
        // Get input from controller
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Get base movement direction
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Check if currently moving
        if (direction.magnitude >= 0.1f)
        {
            // Get direction angle
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Get final movement direction
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Move character
            playerController.Move(moveDir.normalized * walkSpeed * Time.deltaTime);
        }
    }

    // Method to implement gravity to the character
    void Gravity()
    {
        // Apply some downward force while character is grounded
        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
    }

    // Method to check whether the character is grounded or not
    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, checkSphereRadius, groundMask);
    }

    // Method to draw the ground check sphere
    private void OnDrawGizmosSelected()
    {
        if(groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, checkSphereRadius);
        }
    }
}
