using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputNumbersManager : MonoBehaviour
{
  public TMP_InputField[] inputFields;
    public TMP_Text errorMessageText;

    private void Start()
    {
        SetupInputFields();
    }

    private void SetupInputFields()
    {
        // Attach listeners to each TMP_InputField's onValueChanged event
        foreach (var inputField in inputFields)
        {
            inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
            inputField.characterLimit = 1;
        }

        // Set focus on the first input field
        inputFields[0].Select();
    }

    private void Update()
    {
        // Check for backspace key press to handle deletion of the previous character
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("DELETE");
            int currentIndex = GetCurrentInputFieldIndex();

            if (currentIndex > 0 && inputFields[currentIndex].text.Length == 0)
            {
                inputFields[currentIndex - 1].Select();
                inputFields[currentIndex - 1].text = "";
            }
        }
    }

    private void OnInputFieldValueChanged(string newValue)
    {
        if (newValue.Length == 1)
        {
            // Get the current input field index
            int currentIndex = GetCurrentInputFieldIndex();

            // Move focus to the next input field if the current one is filled
            if (currentIndex < inputFields.Length - 1 && inputFields[currentIndex].text.Length == 1)
                inputFields[currentIndex + 1].Select();
        }

        CheckIfCodeIsCorrect();
    }

    private int GetCurrentInputFieldIndex()
    {
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (inputFields[i].isFocused)
                return i;
        }

        return -1;
    }

    private void CheckIfCodeIsCorrect()
    {
        bool isCodeCorrect = true;

        foreach (var inputField in inputFields)
        {
            if (inputField.text.Length != 1)
            {
                isCodeCorrect = false;
                break;
            }
        }

        if (isCodeCorrect)
        {
            string enteredCode = "";
            foreach (var inputField in inputFields)
            {
                enteredCode += inputField.text;
            }

            string correctCode = "1234"; // Replace with the correct code

            if (enteredCode == correctCode)
            {
                // Code is correct
                errorMessageText.text = "";
                // Perform any desired actions or scene transitions
            }
            else
            {
                // Code is incorrect
                errorMessageText.text = "Incorrect code. Please try again.";
                ClearInputFields();
            }
        }
    }

    private void ClearInputFields()
    {
        foreach (var inputField in inputFields)
        {
            inputField.text = "";
        }

        // Set focus back to the first input field
        inputFields[0].Select();
    }
}
