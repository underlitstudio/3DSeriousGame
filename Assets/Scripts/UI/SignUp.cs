using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class SignUpManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField emailInput;
    public TMP_InputField levelInput;
    public TMP_InputField passwordInput;

    private const string SignUpUrl = "http://localhost:9090/user/signUp";

    public void SignUpButtonClicked()
    {
        StartCoroutine(SignUpRoutine());
    }

    private IEnumerator SignUpRoutine()
    {
        string username = usernameInput.text;
        string email = emailInput.text;
        string level = levelInput.text;
        string password = passwordInput.text;

        // Create the JSON payload
        string jsonPayload = "{\"username\":\"" + username + "\", \"email\":\"" + email + "\", \"level\":\"" + level + "\", \"password\":\"" + password + "\"}";

        // Create the request
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(SignUpUrl, "POST"))
        {
            byte[] payloadBytes = new System.Text.UTF8Encoding().GetBytes(jsonPayload);

            request.uploadHandler = new UploadHandlerRaw(payloadBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Sign-up request failed: " + request.error);
            }
            else
            {
                Debug.Log("Sign-up request successful");

                // Handle the response data
                string responseJson = request.downloadHandler.text;
                // Parse the JSON response and process it in your game logic

                Debug.Log("Sign-up response: " + responseJson);
            }
        }
    }
}
