using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Projectile_Type type;
    private Vector2 direction;
    private float speed;
    private float damage;
    [SerializeReference] private SpriteRenderer spriteRenderer;
    private GameObject owner;
    private Weapon_Upgrades weaponUpgrades;

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
                    Sound_Manager.instance.PlaySound(Sound_Manager.SFX.EnemyHit, hit.collider.transform, 1f);
                    hit.collider.gameObject.GetComponent<Enemy_Base>().Damage(GetDamage() * (1 - hit.collider.gameObject.GetComponent<Enemy_Base>().damageReduction));
                }
                if (hit.collider.gameObject.tag == "Player")
                {
                    // Damage Player
                    hit.collider.gameObject.GetComponent<Health>().Damage(GetDamage());
                }
                else if (hit.collider)
                {
                    // Play Laser Rebound
                    Sound_Manager.instance.PlaySound(Sound_Manager.SFX.LaserRebound, transform, 0.1f);
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

    public void SetType(Projectile_Type type)
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

    public void SetWeaponUpgrades(Weapon_Upgrades upgrades)
    {
        weaponUpgrades = upgrades;
    }

    public float GetDamage()
    {
        return weaponUpgrades != null ? damage * weaponUpgrades.damageModifier : damage;
    }
}
