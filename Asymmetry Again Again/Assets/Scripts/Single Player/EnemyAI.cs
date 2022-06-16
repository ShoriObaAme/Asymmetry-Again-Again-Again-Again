using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState 
{
    CHASING, //When the enemy is chasing the player.
    ATTACKING, //When the enemy is attacking the player.
    DEAD, //When the enemy is dead.
 }

public enum EnemyType
{
    STALKER,
    PYROMANCER,
    STUN_LOCK_DRONE,
    KAMIKAZE,
}
public class EnemyAI : MonoBehaviour
{
    [Header("Enemy State and Enemy Type")]
    [SerializeField, Tooltip("What is the enemy's current state?")]private EnemyState enemyState;
    [SerializeField, Tooltip("What is the enemy's type?")]private EnemyType enemyType;

    [Header("Stats")]
    [SerializeField] public EnemyStats enemyStats;
    [SerializeField] private float attackRadius;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private Vector3 Offset;
    [SerializeField] private LayerMask WhatIsPlayer;
    [SerializeField] private Transform Target;

    [Header("Nav Mesh Agent")]
    private NavMeshAgent agent;

    [Header("EnemyHealth")]
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private bool HealthBelowHalf;
    [SerializeField] private bool HealthAnimFinished;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    [Header("State Checkers")]
    [SerializeField] private bool PlayerInAttackRange;

    [Header("Attack Bools")]
    [SerializeField] private bool hasAttacked;

    [Header("Debugging")]
    [SerializeField] private Color DebugColor;

	private void Start()
	{
        Target = GameObject.Find("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        AssignStats();
	}

	// Update is called once per frame
	void Update()
    {
        if (enemyHealth.currentHealth <= (enemyStats.Health / 2))
		{
            HealthBelowHalf = true;
		}
        if (HealthBelowHalf && !HealthAnimFinished)
		{
            HealthAnim();
		}
        PlayerInAttackRange = Physics.CheckSphere(transform.position + Offset, attackRadius, WhatIsPlayer);
        if (!PlayerInAttackRange || (enemyType == EnemyType.STALKER && !PlayerInAttackRange)) ChasePlayer();
        else AttackPlayer();
    }

    private void ChasePlayer()
	{
        Debug.Log("Chasing.");
        enemyState = EnemyState.CHASING;
        agent.SetDestination(Target.position);
        if (!HealthBelowHalf)
        {
            animator.Play("Chasing Player");
        }
        else animator.Play("Stalker_Chasing_Player_Low_Health");

	}

    void HealthAnim()
	{
        
        animator.Play("Stalker_Health_Low");
        HealthAnimFinished = true;
	}

    private void AttackPlayer()
	{
        agent.SetDestination(transform.position);
        if (!hasAttacked)
		{
            Debug.Log("Attacking.");
            enemyState = EnemyState.ATTACKING;
            if (enemyType == EnemyType.STALKER)
            {
                if (!HealthBelowHalf)
                {
                    animator.Play("Stalker_Attack");
                    hasAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                }
                else
                {
                    animator.Play("Stalker_Health_Low_Attack");
                    hasAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                }

            }
        }
	}

	private void OnDrawGizmos()
	{
        Gizmos.color = DebugColor;
        Gizmos.DrawWireSphere(transform.position + Offset, attackRadius);
	}

    private void AssignStats()
	{
        attackRadius = enemyStats.AttackRange;
        agent.speed = enemyStats.MoveSpeed;
        timeBetweenAttacks = enemyStats.TimeBetweenAttacks;
	}

    private void ResetAttack()
	{
        Debug.Log("Resetting Attack");
        hasAttacked = false;
	}
}
