using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orient;
    public Transform camHolder;
    public float fovTransitionSpeed = 5f; // Speed of FOV transition

    float xRotation;
    float yRotation;

    private Camera cam;
    private float currentFov;

    private void Start()
    {
        //locking the cursor to the middle of the screen and hiding it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize the rotation to look straight ahead
        cam = GetComponent<Camera>();
        currentFov = cam.fieldOfView;
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
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orient.rotation = Quaternion.Euler(0, yRotation, 0);

        // Smoothly transition to the target FOV
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, currentFov, Time.deltaTime * fovTransitionSpeed);
    }

    public void DoFov(float fov)
    {
        currentFov = fov;
    }

    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }
    private void OnEnable()
    {
        FollowerScript.onGameOver += FreezeCamera;
    }

    private void OnDisable()
    {
        FollowerScript.onGameOver -= FreezeCamera;
    }

    private void FreezeCamera()
    {
        sensX = 0;
        sensY = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
