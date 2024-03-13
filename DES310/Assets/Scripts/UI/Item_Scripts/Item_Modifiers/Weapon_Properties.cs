using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu (menuName = "Weapon Properties/Weapon Properties")]
public class Weapon_Properties : ScriptableObject
{
    public Projectile_Type projectileType;
    public Projectile projectilePrefab;
    public float fireDelay = 0;
    public float overheatCapacity = 0;
    public bool isSingleFire = false;
    public Weapon_Upgrades weaponUpgrades;

    private void OnEnable()
    {
        weaponUpgrades = CreateInstance<Weapon_Upgrades>();
    }
    public virtual void Fire(Vector2 position, Vector2 direction, GameObject owner)
    {
        // Fire
        Projectile newProjectile = Instantiate(projectilePrefab, position, Quaternion.identity);
        newProjectile.SetType(projectileType);
        newProjectile.SetWeaponUpgrades(weaponUpgrades);
        newProjectile.SetDirection(direction);
        newProjectile.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        newProjectile.SetOwner(owner);
        // Set projectile to despawn after a certain time has elapsed
        Destroy(newProjectile.gameObject, projectileType.despawnTimer * weaponUpgrades.fireRangeModifier);
    }
}
