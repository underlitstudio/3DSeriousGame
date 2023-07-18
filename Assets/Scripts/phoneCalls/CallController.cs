using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CallController : MonoBehaviour
{
    public TMP_Text notificationText;
     private string[] notifications = {
        " patient case number 1",
        " patient case number 2",
        " patient case number 3",
        " patient case number 4",
        " patient case number 5",
        " patient case number 6"
    };
    private List<string> acceptedNotifications = new List<string>();

    private void Start()
    {
        gameObject.SetActive(false); // Set the notification UI to inactive by default
        Invoke("DisplayDelayedNotification", 20f); // Invoke the delayed notification display after 20 seconds
    }

    public void AcceptNotification()
{
    string currentNotification = notificationText.text;
    if (!acceptedNotifications.Contains(currentNotification))
    {    

        acceptedNotifications.Add(currentNotification);
        Debug.Log("Accepted notification: " + currentNotification); // Log the accepted notification
    }

    if (acceptedNotifications.Count < 3)
    {    
        gameObject.SetActive(false);
        Invoke("DisplayDelayedNotification", 20f); // Invoke the delayed notification display after 20 seconds
    }
    else
    {
        Debug.Log("You have accepted three notifications.");
        // Perform any necessary actions when three notifications are accepted.
    }
}


    public void DeclineNotification()
    {    
        gameObject.SetActive(false);
        Invoke("DisplayDelayedNotification", 20f); // Invoke the delayed notification display after 20 seconds
    }

    private void DisplayDelayedNotification()
    {
        gameObject.SetActive(true); // Activate the notification UI
        DisplayRandomNotification(); // Display a random notification
    }

    private void DisplayRandomNotification()
    {
        string randomNotification = GetRandomUnacceptedNotification();
        notificationText.text = randomNotification;
    }

    private string GetRandomUnacceptedNotification()
    {
        List<string> unacceptedNotifications = new List<string>();
        foreach (string notification in notifications)
        {
            if (!acceptedNotifications.Contains(notification))
            {
                unacceptedNotifications.Add(notification);
            }
        }

        if (unacceptedNotifications.Count > 0)
        {
            int randomIndex = Random.Range(0, unacceptedNotifications.Count);
            return unacceptedNotifications[randomIndex];
        }
        else
        {
            return "No more notifications";
        }
    }
}

