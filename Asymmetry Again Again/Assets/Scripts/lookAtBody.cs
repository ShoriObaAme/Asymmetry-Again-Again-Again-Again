using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtBody : MonoBehaviour
{
	[SerializeField]private Transform playerBody;
	public bool IsPlayerDead = false;

	private void Update()
	{
		if (IsPlayerDead)
		{
			LookAtBody();
		}
	}

	private void LookAtBody()
	{
		Debug.Log("Looking at body");
		transform.LookAt(playerBody);
		transform.parent = null;
	}
}
