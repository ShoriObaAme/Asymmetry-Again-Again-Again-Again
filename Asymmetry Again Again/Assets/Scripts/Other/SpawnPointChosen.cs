using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointChosen : MonoBehaviour
{
	public bool isChosen;
	public bool grounded;
	public LayerMask whatIsGround;
	public Color DebugColor;
	private SpawnPoints spawnPoint;

	private void Start()
	{
		spawnPoint = SpawnPoints.SP;
	}

	public void Update()
	{
		grounded = Physics.CheckSphere(transform.position, .25f, whatIsGround);
		if (!grounded)
		{
			spawnPoint.spawnPoints.Remove(this.gameObject);
			gameObject.SetActive(false);
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = DebugColor;
		Gizmos.DrawSphere(transform.position, .25f);
	}
}
