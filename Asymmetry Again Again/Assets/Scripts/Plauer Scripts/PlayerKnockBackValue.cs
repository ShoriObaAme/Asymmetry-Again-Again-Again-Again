using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockBackValue : MonoBehaviour
{
	public float currentKnockBackValue = 0f;
	[SerializeField] private Rigidbody rb;
	[SerializeField] private float knockBackAmount;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		knockBackAmount = (currentKnockBackValue / 10);
	}

	public void LaunchPlayer(Transform origin)
	{
		Debug.Log("Launching Player");
		rb.AddForce(-origin.forward * knockBackAmount, ForceMode.Impulse);
		rb.AddForce(transform.up * knockBackAmount, ForceMode.Impulse);
	}
}
