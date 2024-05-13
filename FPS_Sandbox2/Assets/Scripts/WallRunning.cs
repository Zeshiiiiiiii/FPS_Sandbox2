using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float maxWallRunTime;
    private float wallRunTimer;
    
    [Header("Input")]
    private float horizontalInput;
    private float verticalInput;


    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;


    public Transform orientation;
    private PlayerMovement pm;
    private Rigidbody rb;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update(){
        CheckForwall();
        StateMachine();
        Debug.Log(pm.wallrun);
    }

    private void FixedUpdate(){
        if(pm.wallrun){
            WallRunMovement();
        }
    }

    private bool AboveGround(){
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void CheckForwall(){
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
    }

    private void StateMachine(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if((wallLeft || wallRight) && verticalInput > 0 && AboveGround()){
            if(!pm.wallrun){
                StartWallRun();
            }
        }
        else{
            StopWallRun();
            rb.useGravity = true;
        }

    }

    private void StartWallRun(){
        pm.wallrun = true;
    }

    private void StopWallRun(){
        pm.wallrun = false;
    }

    private void WallRunMovement(){

        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);
    }
}
