using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public CharacterController controller;
    public float sensitivity;
    float xRotation = 0f;

    public bool control = true;
    Vector3 lockOnPoint;

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (control) {
            xRotation -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            controller.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime);
        }
    }
}
