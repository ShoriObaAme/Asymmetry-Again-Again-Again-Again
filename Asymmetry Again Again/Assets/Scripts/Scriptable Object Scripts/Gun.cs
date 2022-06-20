using UnityEngine;
[CreateAssetMenu(menuName = ("ScriptableObjects/Gameplay/Guns/New Gun"), fileName = "New Gun")]
public class Gun : ScriptableObject
{
    public WeaponType WeaponType;
    public string WeaponName;
    public GameObject ProjectilePrefab;
    public int TotalAmmo, StartingAmmo, Damage;
    public float spread, range, timeBetweenShots, ProjectilesPerShot, ReloadTime, timeBetweenCanShootAgain, bulletForce;
    public bool allowWeaponHold;
}
