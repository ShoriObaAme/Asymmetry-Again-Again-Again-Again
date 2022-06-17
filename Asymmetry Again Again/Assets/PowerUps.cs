using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[RequireComponent(typeof(SphereCollider))]
public class PowerUps : MonoBehaviour
{
	[Header("Power Up Type")]
	[HideInInspector]
	public bool Shield;

	[HideInInspector]
	public bool Health;

	[HideInInspector]
	public bool Life;

	[HideInInspector]
	public bool Ammo;
	[HideInInspector]
	public Gun gun;
	[HideInInspector]
	public int AmmoToRestoreClip;
	[HideInInspector]
	public int AmmoToRestoreMag;

	[Header("Reset Settings")]
	public GameObject meshHolder;
	[SerializeField] private SphereCollider SC;
	private UpdatedCharacterController UCC;
	public float PowerUpResetTimeBase;
	public int ResetTimeMulti;
	[SerializeField] private float TotalResetTime;

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			UCC = other.GetComponent<UpdatedCharacterController>();
			CalculateResetTime();
			GainPowerUpType();
			Debug.Log("This Works");
			SC.enabled = false;
			meshHolder.SetActive(false);
			StartCoroutine(PowerUpResetTime(TotalResetTime));
		}
	}

	private void GainPowerUpType()
	{
		if (Shield)
		{
			DoShield();
		}
		else if (Ammo)
		{
			DoAmmo();
		}
		else if (Health)
		{
			DoHealth();
		}
		else if (Life)
		{
			DoLife();	
		}
	}

	public IEnumerator PowerUpResetTime(float resetTime) 
	{
		yield return new WaitForSeconds(resetTime);
		meshHolder.SetActive(true);
		SC.enabled = true;
		yield break;
	}

	private void CalculateResetTime()
	{
		if (Shield)
		{
			TotalResetTime = (PowerUpResetTimeBase * ResetTimeMulti);
		}
		else if (Health)
		{
			TotalResetTime = (PowerUpResetTimeBase * ResetTimeMulti);
		}
		else if (Ammo)
		{
			TotalResetTime = (PowerUpResetTimeBase * ResetTimeMulti);
		}
		else if (Life)
		{
			TotalResetTime = (PowerUpResetTimeBase * ResetTimeMulti);
		}
	}

	private void DoShield()
	{
		if (UCC.isShieldActive == false)
		{
			UCC.Shield();
		}
		ResetUCC();
		return;
	}

	private void DoAmmo()
	{
		Debug.Log("Restoring Ammo");
		ResetUCC();
		return;
	}

	private void DoHealth()
	{
		if (UCC.CurrentKnockbackVlaue > 0)
		{
			UCC.CurrentKnockbackVlaue = 0;
		}
		ResetUCC();
		return;
	}

	private void DoLife()
	{
		if (UCC.Lives < UCC.MaxLives)
		{
			UCC.AddLife();
		}
		ResetUCC();
		return;
	}

	private void ResetUCC()
	{
		UCC = null;
		return;
	}

}
#if UNITY_EDITOR
[CustomEditor(typeof(PowerUps))]
public class PowerUpEditor: Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		PowerUps powerUps = (PowerUps)target;

		powerUps.Shield = EditorGUILayout.Toggle("Shield", powerUps.Shield);
		if (powerUps.Shield)
		{
			Debug.Log("Power up is now of Shield Type");
			powerUps.Life = false;
			powerUps.Health = false;
			powerUps.Ammo = false;
		}

		powerUps.Health = EditorGUILayout.Toggle("Health", powerUps.Health);
		if (powerUps.Health)
		{
			Debug.Log("Power up is now of Health Type");
			powerUps.Life = false;
			powerUps.Shield = false;
			powerUps.Ammo = false;
		}

		powerUps.Life = EditorGUILayout.Toggle("Life", powerUps.Life);
		if (powerUps.Life)
		{
			Debug.Log("Power up is now of Life Type");
			powerUps.Health = false;
			powerUps.Shield = false;
			powerUps.Ammo = false;
		}

		powerUps.Ammo = EditorGUILayout.Toggle("Ammo", powerUps.Ammo);
		if (powerUps.Ammo)
		{
			Debug.Log("Power up is now of Ammo Type");
			powerUps.gun = EditorGUILayout.ObjectField("Gun", powerUps.gun, typeof(Gun), true) as Gun;
			powerUps.AmmoToRestoreClip = EditorGUILayout.IntField("Ammo To Restore Clip", powerUps.AmmoToRestoreClip);
			powerUps.AmmoToRestoreMag = EditorGUILayout.IntField("Ammo To Restore Mag", powerUps.AmmoToRestoreMag);
			powerUps.Life = false;
			powerUps.Health = false;
			powerUps.Shield = false;
		}
	}
}
#endif