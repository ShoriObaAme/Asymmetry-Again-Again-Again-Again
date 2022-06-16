using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(TeleportCheck))]
[RequireComponent(typeof (CapsuleCollider))]

public class PlayerCharacterController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private KnockBack KB;
    [SerializeField] private MatchManager MM;

    [Header("Ground Check Attributes")]
    private float GroundCheckRadius;
    private bool isGrounded;
    [SerializeField]private LayerMask WhatIsGround;

    [Header("Camera")]
    [SerializeField] private Transform WeaponHolder;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerCam;
    public float cameraSensitivity;
    [SerializeField] private float divideFactor;
    [SerializeField] private float maxAngle = 90f;
    [SerializeField] private float xRotation;


    [Header("Movement")]
    public float movementSpeed;
    public int moveSpeedMultiplier;

    [Header("Jumping")]
    public float jumpForce;
    public float gravityValue;

    [Header("Debugging")]
    public Color DebugColor;

    private SpawnPoints spawnPoints;

    [SerializeField] private Vector2 move = Vector2.zero;
    [SerializeField] private Vector3 direction;
    [SerializeField] private Vector2 mouseVector = Vector2.zero;
	// Start is called before the first frame update

	private void Awake()
	{
        GetComponents();
        spawnPoints.RespawnPlayer(gameObject);
	}
	void Start()
    {
        //spawnPoints.RespawnPlayer(gameObject);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        MM.players.Add(this.gameObject);
    }

	private void Update()
	{
        if (!isGrounded)
        {
            rb.AddForce(-transform.up * gravityValue, ForceMode.Impulse);
        }
    }

	// Update is called once per frame
	void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(transform.position, GroundCheckRadius, WhatIsGround);
        CheckYVelocity();
        PlayerMovement();
        LookAtMouse();
    }

    private void GetComponents()
	{
        spawnPoints = GameObject.Find("Game Wide Manager").GetComponent<SpawnPoints>();
        MM = GameObject.Find("Game Wide Manager").GetComponent<MatchManager>();
        return;
	}

    private void OnJump()
	{
        if (isGrounded)
        {
            Jump(jumpForce);
        }
        return;
	}

    private void OnMovement(InputValue value)
	{
        move = value.Get<Vector2>();
	}

    private void OnCamera(InputValue value)
	{
        mouseVector = value.Get<Vector2>();
	}

    private void Jump(float JumpForce)
	{
        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        return;
    }

	private void OnDrawGizmosSelected()
	{
        Gizmos.color = DebugColor;
        Gizmos.DrawSphere(transform.position, GroundCheckRadius);
	}

    private void PlayerMovement()
	{
        direction = gameObject.transform.right * move.x + gameObject.transform.forward * move.y;
        rb.AddForce(direction * (movementSpeed * moveSpeedMultiplier) * Time.fixedDeltaTime);
	}

    private void LookAtMouse()
	{
        Vector3 rotation = gameObject.transform.localRotation.eulerAngles;
        float xTo = rotation.y + mouseVector.x;
        xRotation -= mouseVector.y;
        xRotation = Mathf.Clamp(xRotation, -maxAngle, maxAngle);
        playerCam.localRotation = Quaternion.Euler(xRotation, 0, 0);
        gameObject.transform.localRotation = Quaternion.Euler(0f, xTo, 0);
	}

    public void JumpPadJump(float jumpForce, Transform origin)
	{
        rb.AddForce(origin.up * jumpForce, ForceMode.Impulse);
        return;
	}

    private void OnRespawn()
	{
        spawnPoints.RespawnPlayer(gameObject);
        return;
	}

    private void CheckYVelocity()
	{
        if (transform.position.y < -10)
		{
            KB.Die();
		}
	}
}
