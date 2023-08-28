using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using TMPro;
public class PopulateScrollView : MonoBehaviour
{
    public GameObject cardItemPrefab;
    public Transform scrollViewContent;
    public string apiUrl = "http://localhost:9090/user/readQuestions";
    public string authToken = "YOUR_AUTH_TOKEN"; // Replace with your actual token


    [System.Serializable]
    private class CardItem
    {
        public string username;
        public string level;
    }

    [System.Serializable]
    private class ApiResponse
    {
        public List<CardItem> data;
    }

    private void Start()
    {
        StartCoroutine(FetchDataAndPopulate());
    }

    private IEnumerator FetchDataAndPopulate()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            webRequest.SetRequestHeader("Authorization", "Bearer " + authToken); // Set the authorization header
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error while fetching data: " + webRequest.error);
                yield break;
            }

            string jsonResponse = webRequest.downloadHandler.text;
            ApiResponse apiResponse = JsonUtility.FromJson<ApiResponse>("{\"data\":" + jsonResponse + "}");

            foreach (CardItem cardItem in apiResponse.data)
            {
                Debug.Log("Processing card item for: " + cardItem.username);

                GameObject newCardItem = Instantiate(cardItemPrefab, scrollViewContent);
                if (newCardItem == null)
                {
                    Debug.LogError("Failed to instantiate new card item.");
                    continue;
                }

                TMP_Text nomText = newCardItem.transform.Find("nom")?.GetComponent<TMP_Text>();
                TMP_Text levelNumberText = newCardItem.transform.Find("levelNumber")?.GetComponent<TMP_Text>();

                if (nomText != null && levelNumberText != null)
                {
                    nomText.text = cardItem.username;
                    levelNumberText.text = cardItem.level;
                }
                else
                {
                    Debug.LogError("Child objects 'nom' or 'levelNumber' not found.");
                }
            }
        }
    }
}
