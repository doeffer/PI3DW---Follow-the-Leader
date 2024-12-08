using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallRunning : MonoBehaviour
{
    
    [Header("Wall Running")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float wallJumpUpForce;
    public float wallJumpSideForce;
    public float wallClimbSpeed;
    public float maxWallRunTime;
    public float wallRunTimer;

    [Header("Input")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode upwardsRunKey = KeyCode.LeftShift;
    public KeyCode downwardsRunKey = KeyCode.LeftControl;
    public bool upwardsRunning;
    public bool downwardsRunning;
    private float horiInput;
    private float vertInput;

    [Header("Wall Running Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool isWallLeft;
    private bool isWallRight;

    [Header("Exiting Wall Running")]
    private bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    [Header("Gravity")]
    public bool useGravity;
    public float gravityCounterForce;

    [Header("References")]
    public Transform orient;
    public PlayerCam cam;
    private PlayerMovement pm;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        checkforWall();
        StateMachine();

        // Adjust camera FOV based on speed
        float speed = rb.velocity.magnitude;
        float targetFov = Mathf.Lerp(85f, 120f, speed / 10f); // Adjust the range as needed
        cam.DoFov(targetFov);
    }

    void FixedUpdate()
    {
        if (pm.wallrunning)
            WallRunningMovement();
    }

    private void checkforWall()
    {
        isWallLeft = Physics.Raycast(transform.position, -orient.right, out leftWallhit, wallCheckDistance, whatIsWall);
        isWallRight = Physics.Raycast(transform.position, orient.right, out rightWallhit, wallCheckDistance, whatIsWall);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, -transform.up, minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        //Gathering input
        horiInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");

        upwardsRunning = Input.GetKey(upwardsRunKey);
        downwardsRunning = Input.GetKey(downwardsRunKey);

        //Wall running
        if((isWallLeft || isWallRight) && vertInput > 0 && AboveGround() && !exitingWall)
        {
            if (!pm.wallrunning)
                StartWallRun(); 

            if(wallRunTimer > 0)
                wallRunTimer -= Time.deltaTime;

            if(wallRunTimer <= 0 && pm.wallrunning)
            {
                exitingWall = true;
                exitWallTimer = exitWallTime;

            if(isWallLeft || isWallRight) 
                exitWallTimer = exitWallTime;
            }

           

            //wall jump
            if (Input.GetKeyDown(jumpKey))
                WallJump();
        }

        //Exiting wall
        else if(exitingWall)
        {
            if (pm.wallrunning)
                StopWallRun();

            if(exitWallTimer > 0)
                exitWallTimer -= Time.deltaTime;
            
            if(exitWallTimer <= 0)
                exitingWall = false;
        }

        else
        {
            if (pm.wallrunning)
                StopWallRun();
        }
    }

    private void StartWallRun()
    {
        pm.wallrunning = true; 

        wallRunTimer = maxWallRunTime;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        cam.DoFov(90f);
        if(isWallLeft) cam.DoTilt(-10f);
        if(isWallRight) cam.DoTilt(10f);
    }

    private void WallRunningMovement()
    {
        rb.useGravity = useGravity;
    
        Vector3 wallNormal = isWallRight ? rightWallhit.normal : leftWallhit.normal;
    
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);
    
        if((orient.forward - wallForward).magnitude > (orient.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        //Forward force
        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        if(upwardsRunning)
            rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);

        if(downwardsRunning)
            rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);

        if(!(isWallLeft && horiInput > 0) && !(isWallRight && horiInput < 0))
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
    }

    

    private void StopWallRun()
    {
        pm.wallrunning = false;
        rb.useGravity = true;

        cam.DoFov(80f);
        cam.DoTilt(0f);
    
    }

    private void WallJump()
    {
        exitingWall = true;
        exitWallTimer = exitWallTime;
        Vector3 wallNormal = isWallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;


        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);

    }

}
