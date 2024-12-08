using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float wallrunSpeed;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]

    //Implemented to check if the player is grounded, needed to implement drag.
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orient;

    float horiInput;
    float vertInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public moveState state;
    public enum moveState
    {
        walking,
        sprinting,
        wallrunning,
        air
    }

    public bool wallrunning;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    //Using FixedUpdate instead, as putting this into Update accelerates the player far more, bordering on uncontrollable.
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
      {
        //Checking if the player is grounded, if so, drag is applied to the player.
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        LimitSpeed();
        StateHandler();

        //If the player is grounded, drag is applied to the player.
        if (grounded)
            rb.drag = groundDrag;
    
        else
            rb.drag = 0;

      }

    private void MyInput()
    {
       horiInput = Input.GetAxisRaw("Horizontal");
       vertInput = Input.GetAxisRaw("Vertical");

       if(Input.GetKey(jumpKey) && readyToJump && grounded)
       {
            Jump();
            readyToJump = false;
            
            //Used for continuous jumping, as the player can jump again after the cooldown.
            Invoke(nameof(ResetJump), jumpCooldown);
       }
    }
    private void LimitSpeed()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed * 1.25f)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed * 1.25f;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void StateHandler()
    {
        //Wallrunning
        if(wallrunning)
        {
            state = moveState.wallrunning;
            moveSpeed = wallrunSpeed;
        }

        //Sprinting
       if(grounded && Input.GetKey(sprintKey))
       {
            state = moveState.sprinting;
            moveSpeed = sprintSpeed;
       }

        //Walking
       else if (grounded)
       {
            state = moveState.walking;
            moveSpeed = walkSpeed;
       }

       //Jumping
       else
       {
            state = moveState.air;
       }
    }

    private void MovePlayer()
    {
        moveDirection = orient.forward * vertInput + orient.right * horiInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            // Limit the speed to 1.5 times the max speed when in the air
            if (rb.velocity.magnitude < moveSpeed * 1.5f)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier * 10f, ForceMode.Force);
            }
        }
    }

    

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

}
