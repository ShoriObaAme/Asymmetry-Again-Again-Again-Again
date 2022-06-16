using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallRunning : MonoBehaviour
{/*

    public float WallRunningSpeed;
    public float WallRunMaxTimer;

    public float minJumpHeight;
    public float WallCheckDistance;
    private RaycastHit LeftWallHit;
    private RaycastHit RightWallHit;

	private PlayerInput playerInput;
	//public PlayerControls Playercontrols;
    private Rigidbody rb;
    public Transform orinitation;
    private PlayerCharacterController playermovement;
   // private PlayerJump playerjump;

    public LayerMask WhatisWall;

    public bool isWallRunning, RightWall, LeftWall;


    private void Awake()
    {
        playermovement = GetComponent<PlayerCharacterController>();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
       // playerjump = GetComponent<PlayerJump>();

        //Playercontrols = new PlayerControls();
        //Playercontrols.Gameplay.Enable();
    }

    private void Update()
    {
        CheckForWall();
        StateMachine();
    }

    private void FixedUpdate()
    {
        if (isWallRunning)
        {
            WallRunningMovement();
        }
    }

    private void CheckForWall()
    {
        RightWall = Physics.Raycast(transform.position, orinitation.right, out RightWallHit, WallCheckDistance, WhatisWall);
        LeftWall = Physics.Raycast(transform.position, -orinitation.right, out LeftWallHit, WallCheckDistance, WhatisWall);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, playerjump.groundlayer);
    }
    private void StateMachine()
    {
        if ((LeftWall || RightWall) && AboveGround())
        {
            if(!isWallRunning)
            {
                StartWallRun();
            }
        }
        else
        {
            if (isWallRunning)
            {
                StopWallRun();
            }
        }
    }

    private void StartWallRun()
    {
        isWallRunning = true;
    }

    private void WallRunningMovement()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = RightWall ? RightWallHit.normal : LeftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if((orinitation.forward - wallForward).magnitude > (orinitation.forward - -wallForward).magnitude)
                wallForward = -wallForward;

        rb.AddForce(wallForward * WallRunningSpeed, ForceMode.Force);

        if(!(LeftWall) && !(RightWall))
        {
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }
    }

    private void StopWallRun()
    {
        isWallRunning = false;
    }*/
}

