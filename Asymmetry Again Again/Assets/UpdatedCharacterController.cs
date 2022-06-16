using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(TeleportCheck))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TeleportCheck))]
[RequireComponent(typeof(PlayerIndex))]

public class UpdatedCharacterController : MonoBehaviour
{

    [Header("Components")]
    private Rigidbody rb;

    [Header("Camera")]
    [SerializeField]private Transform weaponHolder;
    [SerializeField] private Transform playerCam;
    public float cameraSensitivity;
    [SerializeField] private float divideFactor;
    [SerializeField] private float maxAngle = 90f;
    [SerializeField] private float xRotation;

    [Header("Ground Check Attributes")]
    [Tooltip("What is the radius of the ground check?")]
    [SerializeField] private float GroundCheckRadius = 0.26f;
    [Tooltip("Is the player currently grounded?")]
    [SerializeField] private bool isGrounded;
    [Tooltip("What is the ground?")]
    [SerializeField] private LayerMask WhatIsGround;
    [Tooltip("GroundCheck Offset")]
    [SerializeField] private Vector3 GroundCheckOffset;

    [Header("Jumping")]
    [Tooltip("How high can the player jump?")]
    public float jumpForce;
    [Tooltip("How much graivty is applied to the player when they are not 'Grounded?'")]
    public float gravityValue;

    [Header("Movement")]
    [Tooltip("How fast can the player move?")]
    public float movementSpeed;
    [Tooltip("How much is the movement speed multiplied by?")]
    public int MovementSpeedMultiplier;

    [Header("Knockback / Health")]
    public int Lives;

    [Header("Debugging")]
    [Tooltip("What is the color of the Ground Check Wire Sphere?")]
    public Color WiresphereColor;

    [SerializeField]Vector2 move = Vector2.zero;
    Vector3 direction;
    Vector2 mouseVector = Vector2.zero;

	private void Start()
	{
        AssignComponents();
	}

    private void AssignComponents()
	{
        rb = GetComponent<Rigidbody>();
        return;
	}
	// Update is called once per frame
	void Update()
    {
        if (!isGrounded)
		{
            rb.AddForce(-transform.up * gravityValue, ForceMode.Impulse);
            Debug.Log("Applying Gravity. Current Force: " + gravityValue);
		}
    }

	private void FixedUpdate()
	{
        isGrounded = Physics.CheckSphere(transform.position + GroundCheckOffset, GroundCheckRadius, WhatIsGround);
        CheckYVelocity();
		PlayerMovement();
        LookAtMouse();
	}

	private void OnJump()
	{
        if (isGrounded)
		{
            Jump(jumpForce);
		}
        return;
	}

	private void OnMovement(InputValue inputValue)
	{
		move = inputValue.Get<Vector2>();
	}

    private void OnCamera(InputValue inputValue)
	{
        mouseVector = inputValue.Get<Vector2>();
	}

	void Jump(float jumpPower)
	{
        rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        return;
	}

    private void PlayerMovement()
	{
        direction = gameObject.transform.right * move.x + gameObject.transform.forward * move.y;
        rb.AddForce(direction * (movementSpeed * MovementSpeedMultiplier) * Time.fixedDeltaTime);
	}

    private void LookAtMouse()
	{
        Vector3 rotation = gameObject.transform.localRotation.eulerAngles;
        float xTo = rotation.y + mouseVector.x;
        xRotation -= mouseVector.y;
        xRotation = Mathf.Clamp(xRotation, -maxAngle, maxAngle);
        playerCam.localRotation = Quaternion.Euler(xRotation, 0, 0);
        gameObject.transform.localRotation = Quaternion.Euler(0, xTo, 0);
        
	}

	private void OnDrawGizmosSelected()
	{
        Gizmos.color = WiresphereColor;
        Gizmos.DrawSphere(transform.position + GroundCheckOffset, GroundCheckRadius);
	}

    private void CheckYVelocity()
	{
        if (transform.position.y < -30)
		{
            Destroy(gameObject);
		}
	}
}
