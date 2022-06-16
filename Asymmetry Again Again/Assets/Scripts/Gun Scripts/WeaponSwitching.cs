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
			EquipPistol();
		}
		else if (CurrentlySelectedWeapon == 1)
		{
			EquipPistol();
		}
	}

	private void EquipPistol()
	{
			WeaponHolder.GetChild(0).gameObject.SetActive(false);
			WeaponHolder.GetChild(1).gameObject.SetActive(true);
			CurrentlySelectedWeapon = 1;
			return;
	}

	private void EquipCannon()
	{
			WeaponHolder.GetChild(1).gameObject.SetActive(false);
			WeaponHolder.GetChild(0).gameObject.SetActive(true);
			CurrentlySelectedWeapon = 0;
			return;
	}
}
