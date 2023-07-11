using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TogglePassword : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button toggleButton;

    private bool isPasswordVisible;

    private void Awake()
    {
        // Set up the button click event
        toggleButton.onClick.AddListener(TogglePasswordVisibility);
    }

    private void TogglePasswordVisibility()
    {
        // Toggle the visibility of the password characters
        isPasswordVisible = !isPasswordVisible;
        inputField.contentType = isPasswordVisible ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
        inputField.ForceLabelUpdate();
    }
}
