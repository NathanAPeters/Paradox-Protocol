using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] Transform playerBody;
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] float cameraHeightOffset;
    private float pitch = 0f;
    CharacterController controller;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = playerBody.GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get mouse input for pitch (up-down)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Adjust camera pitch (up-down)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // Get mouse input for yaw (left-right)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        // Rotate player body horizontally (yaw)
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void LateUpdate()
    {
        // Set camera rotation
        transform.rotation = Quaternion.Euler(pitch, playerBody.eulerAngles.y, 0f);

        // Adjust camera position dynamically
        CharacterController controller = playerBody.GetComponent<CharacterController>();
        float adjustedCameraHeight = controller.height + cameraHeightOffset; // Now camera follows the height properly

        transform.position = playerBody.position + new Vector3(0, adjustedCameraHeight, 0);
    }
}