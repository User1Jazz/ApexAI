using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target; // The object to follow
    public float lookSensitivity = 100f; // Sensitivity for mouse input
    public float distanceFromTarget = 5f; // Distance from the target

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Camera target is not assigned!");
            enabled = false;
            return;
        }

        // Lock cursor for better camera control
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        HandleRotation();
    }

    void LateUpdate()
    {
        FollowTarget();
    }

    void HandleRotation()
    {
        // Get mouse input
        Vector3 mouseInput = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f);

        // Limit vertical rotation
        mouseInput.x = Mathf.Clamp(mouseInput.x, -70f, 70f);

        // Apply rotation
        transform.Rotate(mouseInput * lookSensitivity * Time.deltaTime);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0f);
    }

    void FollowTarget()
    {
        // Position the camera behind the target at the specified distance
        transform.position = target.position - transform.forward * distanceFromTarget;
    }
}
