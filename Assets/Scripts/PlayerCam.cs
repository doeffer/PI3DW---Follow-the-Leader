using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orient;

    float xRotation;
    float yRotation;

    private void Start()
    {
        //locking the cursor to the middle of the screen and hiding it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //getting mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        //clamping the mouse input so that the camera cant do full rotations around itself on the X axis
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate the camera and orientation of player character
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orient.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
