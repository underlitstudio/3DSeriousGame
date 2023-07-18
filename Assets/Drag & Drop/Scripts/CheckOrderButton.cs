using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOrderButton : MonoBehaviour
{
    public DragDropManager dragDropManager; // Reference to the DragDropManager script
    public string[] customOrder; // Define the custom order of object IDs here

    public void CheckOrder()
    {
        // Check if the objects are in the custom-defined order
        bool isCorrectOrder = CheckCustomOrder();

        // Log the result
        if (isCorrectOrder)
        {
            Debug.Log("Correct order");
        }
        else
        {
            Debug.Log("Incorrect order");
        }
    }

   private bool CheckCustomOrder()
{
    // Check if the number of panels matches the custom order length
    if (dragDropManager.AllPanels.Length != customOrder.Length)
    {
        return false; // Incorrect order
    }

    // Iterate through the panels and check if the objects are in the correct order
    for (int i = 0; i < dragDropManager.AllPanels.Length; i++)
    {
        string panelId = dragDropManager.AllPanels[i].Id;
        string objectInPanel = DragDropManager.GetPanelObject(panelId);

        // Compare the object's Id with the custom-defined order
        if (objectInPanel != customOrder[i])
        {
            return false; // Incorrect order
        }
    }

    return true; // Correct order
}

}
