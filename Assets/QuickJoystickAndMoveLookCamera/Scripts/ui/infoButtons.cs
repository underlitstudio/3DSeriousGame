using UnityEngine;
using UnityEngine.UI;

public class InfoButtonBehavior : MonoBehaviour
{
    public Transform mainCamera;
    // public Transform unit;
    //public float yOffset = 1.5f;
    public float followSpeed = 5.0f;

    private void Start()
    {
        mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        // Vector3 targetPosition = unit.position + Vector3.up * yOffset;
        // Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
       // transform.position = new Vector3(smoothedPosition.x, transform.position.y, smoothedPosition.z);

        Vector3 lookDirection = new Vector3(mainCamera.position.x - transform.position.x, 0f, mainCamera.position.z - transform.position.z);
        if (lookDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Euler(0f, newRotation.eulerAngles.y, 0f);
        }
    }
}
