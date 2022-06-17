using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointChosen : MonoBehaviour
{
	public bool isChosen;
	public bool grounded;
	public LayerMask whatIsGround;
	public Color DebugColor;
	public Vector3 offset;
	[SerializeField] private SpawnPoints spawnPoint;

	private void Start()
	{
		spawnPoint = GameObject.Find("Game Manager").GetComponent<SpawnPoints>();
	}

	public void Update()
	{
		grounded = Physics.CheckSphere(transform.position + offset, .25f, whatIsGround);
		if (!grounded)
		{
			spawnPoint.spawnPoints.Remove(this.gameObject);
			gameObject.SetActive(false);
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = DebugColor;
		Gizmos.DrawSphere(transform.position + offset, .25f);
	}
}
