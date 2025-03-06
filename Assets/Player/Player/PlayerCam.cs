using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] Transform playerBody;
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] float cameraHeightOffset;
    [SerializeField] TMP_InputField inputField; // Reference to the TMP Input Field
    private float pitch = 0f;
    CharacterController controller;
    private bool isTyping = false;

    void Start()
    {
        controller = playerBody.GetComponent<CharacterController>();
        LockCursor();
    }

    void Update()
    {
        // Check if the input field is focused
        if (inputField != null && inputField.isFocused)
        {
            isTyping = true;
            UnlockCursor();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            isTyping = false;
            LockCursor();
        }

        // Prevent camera movement when typing
        if (isTyping) return;

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
        float adjustedCameraHeight = controller.height + cameraHeightOffset;
        transform.position = playerBody.position + new Vector3(0, adjustedCameraHeight, 0);
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}