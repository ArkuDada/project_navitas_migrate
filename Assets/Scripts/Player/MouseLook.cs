using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    [SerializeField] private Transform PlayerBody;

    private float xRotation;

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState != CursorLockMode.None)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            PlayerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void CameraReset()
    {
        xRotation = 0;
        transform.localRotation = Quaternion.Euler(0, 0f, 0f);
    }
}