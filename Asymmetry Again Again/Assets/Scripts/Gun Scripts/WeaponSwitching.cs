using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
	[SerializeField] private Transform WeaponHolder;

	[SerializeField] private int CurrentlySelectedWeapon = 0;

	private void OnChangeWeapon()
	{
		if (CurrentlySelectedWeapon == 0)
		{
			EquipCannon();
		}
		else if (CurrentlySelectedWeapon == 1)
		{
			EquipPistol();
		}
	}

	private void OnFire()
	{
		if (CurrentlySelectedWeapon == 0)
		{
			GunScriptUpdated gunScript = WeaponHolder.GetChild(0).GetComponent<GunScriptUpdated>();
			gunScript.CheckShoot();
		}
		else if (CurrentlySelectedWeapon == 1)
		{
			GunScriptUpdated gunScript = WeaponHolder.GetChild(1).GetComponent<GunScriptUpdated>();
			gunScript.CheckShoot();
		}
	}

	private void EquipPistol()
	{
			WeaponHolder.GetChild(0).gameObject.SetActive(true);
			WeaponHolder.GetChild(1).gameObject.SetActive(false);
			CurrentlySelectedWeapon = 0;
			return;
	}

	private void EquipCannon()
	{
			WeaponHolder.GetChild(0).gameObject.SetActive(false);
			WeaponHolder.GetChild(1).gameObject.SetActive(true);
			CurrentlySelectedWeapon = 1;
			return;
	}
}
