/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GunSystem : MonoBehaviour
{
    [Header("gun stats")]
    public Gun GunStats;
    [SerializeField] private int damage;
    [SerializeField] private float timeBetweenShooting;
    [SerializeField] private float spread;
    [SerializeField] private float range;
    [SerializeField] private float reloadTime;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private int totalAmmo;
    [SerializeField] private int magazineSize;
    [SerializeField] private int bulletsPerTap;
    [SerializeField] private int ammoDivideAmount;
    [SerializeField] int bulletsLeft;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] int bulletsShot;
    [Header("bools")]
    public bool allowButtonHold;
    [SerializeField] bool shooting;
    [SerializeField] bool readyToShoot;
    [SerializeField] bool reloading;

    public GameObject bullet;
    public Transform bulletspawn;

    private PlayerInput playerInput;
    public PlayerControls Playercontrols;

    private void AssignStats()
	{
        weaponType = GunStats.WeaponType;
        if (weaponType == WeaponType.DAMAGE)
		{
            damage = GunStats.Damage;
		}
        else
		{
            damage = 0;
		}
        timeBetweenShots = GunStats.timeBetweenShots;
        bullet = GunStats.ProjectilePrefab;
        spread = GunStats.spread;
        range = GunStats.range;
        reloadTime = GunStats.ReloadTime;
        totalAmmo = GunStats.TotalAmmo;
        magazineSize = GunStats.TotalAmmo / ammoDivideAmount;
        timeBetweenShooting = GunStats.timeBetweenCanShootAgain;
        allowButtonHold = GunStats.allowWeaponHold;
	}
    private void Awake()
    {
        AssignStats();
        playerInput = GetComponent<PlayerInput>();
        Playercontrols = new PlayerControls();
        Playercontrols.Gameplay.Enable();

        Playercontrols.Gameplay.Fire.performed += context => FireGun();


    }

    public void FireGun()
    {
        if (readyToShoot && !shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;


            Debug.Log("shooting");
            Shoot();
            Debug.Log("shooting");
        }
    }

    public void Shoot()
    {
        Instantiate(bullet.transform, bulletspawn.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsLeft <= 0)
        {
            Reload();
        }

        if (bulletsShot > 0 && bulletsLeft > 0)

            if (bulletsLeft > 0 && bulletsLeft > 0)
            {
                Invoke("Shoot", timeBetweenShots);
            }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        return;
    }
    public void Reload()
    {
        Debug.Log("Reloading");
        reloading = true;
        Invoke("reloadingFinished", reloadTime);
        return;
    }

    private void reloadingFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        reloading = true;
        Invoke("reloadFinished", reloadTime);
        return;
    }

    private void reloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        return;
    }

}*/
