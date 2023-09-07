using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///Developed By Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

public class UIEvents : MonoBehaviour
{
    public void RestartClickEvent()
    {
        StartCoroutine(SceneLoader.instance.LoadScene(SceneManager.GetActiveScene().name));
    }

    public void ShowTrashConfirmDialog()
    {
        GameManager.instance.DisableGameManager();
        GameObject.Find("TrashConfirmDialog").GetComponent<ConfirmDialog>().Show();
    }

    public void TrashConfirmDialogEvent(GameObject value)
    {
        if (value == null)
        {
            return;
        }

        if (value.name.Equals("YesButton"))
        {
            Debug.Log("Trash Confirm Dialog : Yes button clicked");
            GameManager.instance.CleanScreen();

        }
        else if (value.name.Equals("NoButton"))
        {
            Debug.Log("Trash Confirm Dialog : No button clicked");
        }
        value.GetComponentInParent<ConfirmDialog>().Hide();
        GameManager.instance.EnableGameManager();
    }

    public void LoadMainScene()
    {
        StartCoroutine(SceneLoader.instance.LoadScene("Main"));
    }

    public void LoadLevel(int index)
    {
        StartCoroutine(SceneLoader.instance.LoadScene("Level" + index));
    }

    public void LeaveApp()
    {
        Application.Quit();
    }
}
