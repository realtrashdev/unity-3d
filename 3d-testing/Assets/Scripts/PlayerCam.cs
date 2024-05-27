using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float xSens;
    public float ySens;

    public float fovSmoothing;

    public Transform orientation;
    public PlayerMovement playerMovement;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySens;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        FieldOfView();
    }

    void FieldOfView()
    {
        if (playerMovement.moveSpeed > 2)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 80, fovSmoothing * Time.deltaTime);
        }

        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 70, fovSmoothing * Time.deltaTime);
        }
    }
}
