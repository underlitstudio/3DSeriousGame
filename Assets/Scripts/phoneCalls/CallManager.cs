using UnityEngine;

public class CallManager : MonoBehaviour
{
    public CallController callController;
    private int totalNotifications = 0;

    private void Start()
    {
        DisplayNextNotification();
    }

    public void DisplayNextNotification()
    {
        if (totalNotifications < 3)
        {
            callController.gameObject.SetActive(true);
            totalNotifications++;
        }
        else
        {
            Debug.Log("You have accepted three notifications. No more notifications will be displayed.");
            // Perform any necessary actions when three notifications are accepted.
        }
    }
}