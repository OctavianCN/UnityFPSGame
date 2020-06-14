using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 100.0f;
    [SerializeField] private Transform playerTransform;
    private float xRotation;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (PauseMenu.paused == false)
        {
            float InputX = Input.GetAxis("Mouse X");
            float InputY = Input.GetAxis("Mouse Y");

            xRotation -= InputY;
            xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);
            transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
            playerTransform.Rotate(Vector3.up * InputX);
        }
    }
}
