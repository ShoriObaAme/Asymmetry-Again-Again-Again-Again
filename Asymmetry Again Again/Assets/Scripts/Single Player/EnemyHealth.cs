using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
	public float currentHealth;

	private void Start()
	{
		EnemyStats enemyStats = GetComponent<EnemyAI>().enemyStats;
		currentHealth = enemyStats.Health;
	}

	private void Update()
	{
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		EnemyAI EAI = GetComponent<EnemyAI>();
		EAI.enabled = false;
		NavMeshAgent navMesh = GetComponent<NavMeshAgent>();
		navMesh.enabled = false;
		Animator anim = GetComponent<Animator>();
		anim.enabled = false;
		Rigidbody RB = transform.GetChild(0).GetComponent<Rigidbody>();
		RB.isKinematic = false;
		RB.AddForce(-RB.transform.forward * 5f, ForceMode.Impulse);
		this.enabled = false;
	}
}
