using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneInfoButtons : MonoBehaviour
{
   public GameObject canvasToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            canvasToActivate.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            canvasToActivate.SetActive(false);
        }
    }
}
