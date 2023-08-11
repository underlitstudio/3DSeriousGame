using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CallController : MonoBehaviour
{
    public string notificationText;
    public TMP_Text displayText;

    public ResponseGroupData responseGroupData; // Reference to your ResponseGroupData asset

    public QnAScriptableObject[] qnaScriptableObjects; // Instances of QnAScriptableObject

    private List<ResponseGroupData.ResponseGroup> acceptedResponseGroups = new List<ResponseGroupData.ResponseGroup>();

    private int responsesAccepted = 0; // Counter for accepted responses

    private int[] responseIndices = { 0, 0, 0 }; // Track the index of the response for each scriptable object


    private void Start()
    {
        gameObject.SetActive(false);
        Invoke("DisplayDelayedNotification", 6f);
    }

    public void AcceptNotification()
    {
        ResponseGroupData.ResponseGroup currentResponseGroup = GetResponseGroupByNotificationText(notificationText);
        if (currentResponseGroup != null && !acceptedResponseGroups.Contains(currentResponseGroup))
        {
            acceptedResponseGroups.Add(currentResponseGroup);
            responsesAccepted++;

            Debug.Log("Accepted response group");

            if (responsesAccepted >= 3)
            {
                Debug.Log("You have accepted three response groups.");
                StoreAcceptedResponses();
            }
            else
            {
                gameObject.SetActive(false);
                Invoke("DisplayDelayedNotification", 6f);
            }
        }
    }

    public void DeclineNotification()
    {
        gameObject.SetActive(false);
        Invoke("DisplayDelayedNotification", 6f);
    }

    private void DisplayDelayedNotification()
    {
        gameObject.SetActive(true);
        DisplayRandomNotification();
    }

    private void DisplayRandomNotification()
    {
        ResponseGroupData.ResponseGroup randomResponseGroup = GetRandomUnacceptedResponseGroup();
        notificationText = randomResponseGroup.responses[0];
        displayText.text = randomResponseGroup.additionalText;
    }

    private ResponseGroupData.ResponseGroup GetRandomUnacceptedResponseGroup()
    {
        List<ResponseGroupData.ResponseGroup> unacceptedResponseGroups = new List<ResponseGroupData.ResponseGroup>();
        foreach (ResponseGroupData.ResponseGroup group in responseGroupData.responseGroups)
        {
            if (!acceptedResponseGroups.Contains(group))
            {
                unacceptedResponseGroups.Add(group);
            }
        }

        if (unacceptedResponseGroups.Count > 0)
        {
            int randomIndex = Random.Range(0, unacceptedResponseGroups.Count);
            return unacceptedResponseGroups[randomIndex];
        }
        else
        {
            return new ResponseGroupData.ResponseGroup() { responses = new List<string> { "No more response groups" } };
        }
    }

    private ResponseGroupData.ResponseGroup GetResponseGroupByNotificationText(string notification)
    {
        foreach (ResponseGroupData.ResponseGroup group in responseGroupData.responseGroups)
        {
            if (group.responses.Contains(notification))
            {
                return group;
            }
        }
        return null;
    }

   private void StoreAcceptedResponses()
    {
        for (int i = 0; i < acceptedResponseGroups.Count; i++)
        {
            QnAScriptableObject qnaSO = qnaScriptableObjects[i];
            ResponseGroupData.ResponseGroup group = acceptedResponseGroups[i];

            int responseIndex = responseIndices[i]; // Get the current index for this scriptable object

            foreach (string response in group.responses)
            {
                qnaSO.qnaPairs[responseIndex].response = response; // Assign the response to the appropriate index
                responseIndex = (responseIndex + 1) % qnaSO.qnaPairs.Count; // Circular increment
            }

            responseIndices[i] = responseIndex; // Update the index for the next response group
        }

        // Clear the list of accepted response groups
        acceptedResponseGroups.Clear();
        responsesAccepted = 0;
      }
 }




// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using System.Collections.Generic;

// public class CallController : MonoBehaviour
// {
//     public TMP_Text notificationText;
//      private string[] notifications = {
//         " patient case number 1",
//         " patient case number 2",
//         " patient case number 3",
//         " patient case number 4",
//         " patient case number 5",
//         " patient case number 6"
//     };
//     private List<string> acceptedNotifications = new List<string>();

//     private void Start()
//     {
//         gameObject.SetActive(false); // Set the notification UI to inactive by default
//         Invoke("DisplayDelayedNotification", 20f); // Invoke the delayed notification display after 20 seconds
//     }

//     public void AcceptNotification()
// {
//     string currentNotification = notificationText.text;
//     if (!acceptedNotifications.Contains(currentNotification))
//     {    

//         acceptedNotifications.Add(currentNotification);
//         Debug.Log("Accepted notification: " + currentNotification); // Log the accepted notification
//     }

//     if (acceptedNotifications.Count < 3)
//     {    
//         gameObject.SetActive(false);
//         Invoke("DisplayDelayedNotification", 20f); // Invoke the delayed notification display after 20 seconds
//     }
//     else
//     {
//         Debug.Log("You have accepted three notifications.");
//         // Perform any necessary actions when three notifications are accepted.
//     }
// }


//     public void DeclineNotification()
//     {    
//         gameObject.SetActive(false);
//         Invoke("DisplayDelayedNotification", 20f); // Invoke the delayed notification display after 20 seconds
//     }

//     private void DisplayDelayedNotification()
//     {
//         gameObject.SetActive(true); // Activate the notification UI
//         DisplayRandomNotification(); // Display a random notification
//     }

//     private void DisplayRandomNotification()
//     {
//         string randomNotification = GetRandomUnacceptedNotification();
//         notificationText.text = randomNotification;
//     }

//     private string GetRandomUnacceptedNotification()
//     {
//         List<string> unacceptedNotifications = new List<string>();
//         foreach (string notification in notifications)
//         {
//             if (!acceptedNotifications.Contains(notification))
//             {
//                 unacceptedNotifications.Add(notification);
//             }
//         }

//         if (unacceptedNotifications.Count > 0)
//         {
//             int randomIndex = Random.Range(0, unacceptedNotifications.Count);
//             return unacceptedNotifications[randomIndex];
//         }
//         else
//         {
//             return "No more notifications";
//         }
//     }
// }


