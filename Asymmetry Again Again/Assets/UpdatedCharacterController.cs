using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
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
    [SerializeField] private SpawnPoints spawnPoints;

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
    public int MaxLives;
    public float CurrentKnockbackVlaue;
    [SerializeField] private float KnockBackValueDisplay;
    public bool canPlayerRespawn = true;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI LivesText, KnockbackValueText;


    [Header("Shield")]
    [Tooltip("The shield gameobject")]
    public GameObject shield;
    [Tooltip("The shield Icon")]
    public GameObject shieldIcon;
    [Tooltip("Is the shield active?")]
    public bool isShieldActive;
    

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
        spawnPoints = GameObject.Find("Game Manager").GetComponent<SpawnPoints>();
        return;
	}
	// Update is called once per frame
	void Update()
    {
        Lives = Mathf.Clamp(Lives, 0, MaxLives);
        if (!isGrounded)
		{
            rb.AddForce(-transform.up * gravityValue, ForceMode.Impulse);
            Debug.Log("Applying Gravity. Current Force: " + gravityValue);
		}
        KnockBackValueDisplay = CurrentKnockbackVlaue / 10;
        KnockbackValueText.text = "%" + KnockBackValueDisplay.ToString("000");
        LivesText.text = "Lives: " + Lives.ToString("0");
    }

	private void FixedUpdate()
	{
        isGrounded = Physics.CheckSphere(transform.position + GroundCheckOffset, GroundCheckRadius, WhatIsGround);
        CheckYVelocity();
		PlayerMovement();
        LookAtMouse();
        if (!canPlayerRespawn)
		{
            LookAtBody();
		}
	}

    public void Shield()
	{
        if (!isShieldActive)
		{
            shield.SetActive(true);
            shieldIcon.SetActive(true);
            isShieldActive = true;
		}
        else if (isShieldActive)
		{
            shield.SetActive(false);
            shieldIcon.SetActive(false);
            isShieldActive = false;
		}
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
        if (canPlayerRespawn)
		{
            direction = gameObject.transform.right * move.x + gameObject.transform.forward * move.y;
            rb.AddForce(direction * (movementSpeed * MovementSpeedMultiplier) * Time.fixedDeltaTime);
		}
	}

    private void LookAtMouse()
	{
        if (canPlayerRespawn)
		{
            Vector3 rotation = gameObject.transform.localRotation.eulerAngles;
            float xTo = rotation.y + mouseVector.x;
            xRotation -= mouseVector.y;
            xRotation = Mathf.Clamp(xRotation, -maxAngle, maxAngle);
            playerCam.localRotation = Quaternion.Euler(xRotation, 0, 0);
            gameObject.transform.localRotation = Quaternion.Euler(0, xTo, 0);
		}
        
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
            Die();
		}
	}

    public void AddLife()
	{
        Lives += 1;
        return;
	}

	private void Die()
	{
        if (Lives > 0)
        {
            RespawnDie();
        }
        else PermaDie();
	}

    private void RespawnDie()
	{
        Lives--;
        CurrentKnockbackVlaue = 0;
        spawnPoints.RespawnPlayer(this.gameObject);
        return;
	}

    private void PermaDie()
	{
        canPlayerRespawn = false;
        playerCam.parent = null;
        StartCoroutine(DestroyPlayerBody());
	}

    private IEnumerator DestroyPlayerBody()
	{
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
        yield break;
	}

    private void LookAtBody()
	{
        playerCam.LookAt(gameObject.transform);
    }
}
