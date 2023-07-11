using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class SignIn: MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    private const string SignInUrl = "http://localhost:9090/user/signIn";

    public void SignInButtonClicked()
    {
        StartCoroutine(SignInRoutine());
    }

    private IEnumerator SignInRoutine()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        // Create the request payload
        WWWForm form = new WWWForm();
        form.AddField("emailOrUsername", username);
        form.AddField("password", password);

        // Send the request
        using (UnityWebRequest request = UnityWebRequest.Post(SignInUrl, form))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Sign-in request failed: " + request.error);
            }
            else
            {
                Debug.Log("Sign-in request successful");

                // Handle the response data
                string responseJson = request.downloadHandler.text;
                // Parse the JSON response and process it in your game logic

                // Example: Save sign-in data to PlayerPrefs and log them
                PlayerPrefs.SetString("Username", username);
                PlayerPrefs.SetString("Token", "your-auth-token");

                Debug.Log("Username: " + username);
                Debug.Log("Token: " + "your-auth-token");
            }
        }
    }
}
