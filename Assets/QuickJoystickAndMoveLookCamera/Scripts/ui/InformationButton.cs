using UnityEngine;

public class InformationButton : MonoBehaviour
{
    public GameObject informationScreenCanvas;

    public void ShowInformationScreen()
    {
        informationScreenCanvas.SetActive(true);
    }
}
