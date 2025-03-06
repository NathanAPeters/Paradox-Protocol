using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenChat : MonoBehaviour
{
    [SerializeField] Canvas canvas; // Reference to the chat canvas
    [SerializeField] TMP_InputField inputField; // Reference to the TMP Input Field

    private void Start()
    {
        canvas.enabled = false; // Ensure chat starts hidden
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Insert))
        {
            ToggleChat();
        }
    }

    private void ToggleChat()
    {
        canvas.enabled = !canvas.enabled; // Show or hide chat UI

        if (canvas.enabled)
        {
            inputField.ActivateInputField(); // Focus on the input field
            Cursor.lockState = CursorLockMode.None; // Unlock cursor for typing
            Cursor.visible = true;
        }
        else
        {
            inputField.DeactivateInputField(); // Unfocus input field
            Cursor.lockState = CursorLockMode.Locked; // Lock cursor for gameplay
            Cursor.visible = false;
        }
    }
}
