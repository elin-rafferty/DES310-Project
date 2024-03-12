using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private ProjectileType type;
    private Vector2 direction;
    private float speed;
    private float damage;
    [SerializeReference] private SpriteRenderer spriteRenderer;
    private GameObject owner;
    private WeaponUpgrades weaponUpgrades;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get current position
        Vector2 oldPos = transform.position;
        float distance = speed * Time.deltaTime;
        // Get new desired position
        Vector2 newPos = oldPos + direction * distance;
        RaycastHit2D[] hits;
        // Raycast along travel path
        hits = Physics2D.LinecastAll(oldPos, newPos);
        foreach (RaycastHit2D hit in hits )
        {
            // Don't hit the player that shot the bullet
            if (hit.collider.gameObject != owner.gameObject)
            {
                Destroy(gameObject);

                if (hit.collider.gameObject.tag == "Enemy")
                {
                    SoundManager.instance.PlaySound(SoundManager.SFX.EnemyHit, hit.collider.transform, 1f);
                    hit.collider.gameObject.GetComponent<EnemyBase>().Damage(GetDamage() * (1 - hit.collider.gameObject.GetComponent<EnemyBase>().damageReduction));
                }
                if (hit.collider.gameObject.tag == "Player")
                {
                    // Damage Player
                    hit.collider.gameObject.GetComponent<Health>().Damage(GetDamage());
                }
                else if (hit.collider)
                {
                    // Play Laser Rebound
                    SoundManager.instance.PlaySound(SoundManager.SFX.LaserRebound, transform, 0.1f);
                }
                break;
            }
        }
        // Move to new position
        transform.position = newPos;
    }

    public void SetDirection(Vector2 newDirection)
    {
        // Set direction and normalize
        direction = newDirection;
        direction.Normalize();
        // Reset rotation, then rotate
        transform.rotation = Quaternion.identity;
        transform.Rotate(direction);
    }

    public void SetType(ProjectileType type)
    {
        // Update type and properties
        this.type = type;
        speed = type.speed;
        damage = type.damage;
        spriteRenderer.sprite = type.sprite;
    }

    public void SetOwner(GameObject owner)
    {
        // Track owner to avoid collision with shooter
        this.owner = owner;
    }

    public void SetWeaponUpgrades(WeaponUpgrades upgrades)
    {
        weaponUpgrades = upgrades;
    }

    public float GetDamage()
    {
        return weaponUpgrades != null ? damage * weaponUpgrades.damageModifier : damage;
    }
}
