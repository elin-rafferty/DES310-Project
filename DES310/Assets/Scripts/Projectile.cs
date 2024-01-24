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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 oldPos = transform.position;
        float distance = speed * Time.deltaTime;
        Vector2 newPos = oldPos + direction * distance;
        RaycastHit2D[] hits;
        hits = Physics2D.LinecastAll(oldPos, newPos);
        foreach (RaycastHit2D hit in hits )
        {
            if (hit.collider.gameObject != owner.gameObject)
            {
                Debug.Log("Hit " + hit.collider.gameObject.name);
                Destroy(gameObject);
                break;
            }
        }
        transform.position = newPos;
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
        direction.Normalize();
        transform.rotation = Quaternion.identity;
        transform.Rotate(direction);
    }

    public void SetType(ProjectileType type)
    {
        this.type = type;
        speed = type.speed;
        damage = type.damage;
        spriteRenderer.sprite = type.sprite;
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }
}
