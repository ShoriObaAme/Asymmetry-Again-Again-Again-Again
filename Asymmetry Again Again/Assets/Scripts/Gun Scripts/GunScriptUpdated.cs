using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
#region weaponType
public enum WeaponType
{
    DAMAGE, //Increase the Player's Knockback Value
    KNOCKBACK// Launch the Player.
}
#endregion

public class GunScriptUpdated : MonoBehaviour
{
    [Header("Gun Scriptable Object")]
    [SerializeField] private Gun gun;

    [Header("Gun Stats")]
    public WeaponType weaponType;
    [SerializeField] private float reloadTime, timeBetweenBulletsFired, timeBetweenShots, BulletForce, Spread;
    public int damage, totalAmmo, startingAmmo, currentAmmo, bulletsFiredPerShot, bulletsFired;
    [SerializeField] private bool allowHold, canFire, isReloading;
    [SerializeField] private GameObject BulletOrProjectilePrefab;
    [SerializeField] private Transform BulletOrProjectileFirePoint;

    [Header("Input References")]
    [SerializeField] private PlayerInput playerInput;
    //[SerializeField] private PlayerControls playerControls;
    // Start is called before the first frame update
    void Awake()
    {
        AssignStatistics();
        AssignReferences();
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    private void AssignStatistics()
	{
        weaponType = gun.WeaponType;
        if (weaponType == WeaponType.DAMAGE)
		{
            damage = gun.Damage;
		}
        else if (weaponType == WeaponType.KNOCKBACK)
		{
            Debug.Log("Weapon is a Knockback Weapon, and has no Damage Value.");
		}
        reloadTime = gun.ReloadTime;
        totalAmmo = gun.TotalAmmo;
        startingAmmo = gun.StartingAmmo;
        allowHold = gun.allowWeaponHold;
        Spread = gun.spread;
        BulletForce = gun.bulletForce;
        timeBetweenBulletsFired = gun.timeBetweenShots;
        timeBetweenShots = gun.timeBetweenCanShootAgain;
        BulletOrProjectilePrefab = gun.ProjectilePrefab;
        currentAmmo = startingAmmo;
	}

    private void AssignReferences()
	{
        playerInput = GetComponentInParent<PlayerInput>();
	}

    /*public void OnFire()
	{
        Debug.Log("Running Shoot Method.");
        if (canFire && !isReloading && currentAmmo >= 1)
        {
            
            Shoot();
		}
	}*/

    public void CheckShoot()
	{
        Debug.Log("Running Shoot Method.");
        if (canFire && !isReloading && currentAmmo >= 1)
        {
            Shoot();
        }
    }

    private void Shoot()
	{
        canFire = false;
        //currentAmmo--;
        bulletsFired++;
        GameObject bullet = Instantiate(BulletOrProjectilePrefab, BulletOrProjectileFirePoint.position, BulletOrProjectilePrefab.transform.rotation);
        Rigidbody RB = bullet.GetComponent<Rigidbody>();
        RB.AddForce(BulletOrProjectileFirePoint.forward * BulletForce, ForceMode.Impulse);
        if ((currentAmmo <= 0 && totalAmmo > 0))
		{
            Reload();
		}
        else Invoke("ShotReset", timeBetweenShots);
	}

    public void Reload()
	{
        isReloading = true;
        canFire = false;
        Invoke("FinishReload", reloadTime);
	}

    private void ShotReset()
	{
        if (!isReloading || totalAmmo !<= 0)
		{
            canFire = true;
		}
        return;
	}

    private void FinishReload()
	{
        currentAmmo += startingAmmo;
        totalAmmo -= currentAmmo;
        bulletsFired = 0;
        isReloading = false;
        canFire = true;
        return;
	}

    private void ManualReload()
	{
        if (currentAmmo < startingAmmo && totalAmmo > 0)
        {
            canFire = false;
            isReloading = true;
            Invoke("FinishManualReload", reloadTime);
        }
        else Debug.LogWarning("You cannot reload with a full magazine.");
        return;
	}

    private void FinishManualReload()
	{
        isReloading = false;
        canFire = true;
        totalAmmo -= bulletsFired;
        currentAmmo += (startingAmmo - currentAmmo);
        bulletsFired = 0;
        return;
	}
}
