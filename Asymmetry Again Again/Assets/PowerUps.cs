using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PowerUps : MonoBehaviour
{
	[Header("Power Up Type")]
	[HideInInspector]
	public bool Shield;

	[HideInInspector]
	public bool Health;

	[HideInInspector]
	public bool Ammo;
	[HideInInspector]
	public Gun gun;
	[HideInInspector]
	public int AmmoToRestoreClip;
	[HideInInspector]
	public int AmmoToRestoreMag;
	
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
			powerUps.Health = false;
			powerUps.Ammo = false;
		}

		powerUps.Health = EditorGUILayout.Toggle("Health", powerUps.Health);
		if (powerUps.Health)
		{
			Debug.Log("Power up is now of Health Type");
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
			powerUps.Health = false;
			powerUps.Shield = false;
		}
	}
}
#endif