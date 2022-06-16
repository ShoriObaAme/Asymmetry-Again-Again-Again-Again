using UnityEngine;

[CreateAssetMenu(menuName = ("ScriptableObjects/Gameplay/Enemies"), fileName = "Enemy Stats")]
public class EnemyStats : ScriptableObject
{
	public string Name;
	public string Description;
	public float Health;
	public float MoveSpeed;
	public float AttackRange;
	public float TimeBetweenAttacks;
}
