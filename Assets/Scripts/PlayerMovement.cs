using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed;

    public Transform orient;

    float horiInput;
    float vertInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void MyInput()
    {
       horiInput = Input.GetAxisRaw("Horizontal");
       vertInput = Input.GetAxisRaw("Vertical");
    }

}
