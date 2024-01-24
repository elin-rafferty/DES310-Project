using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private ProjectileType type;
    private Vector2 direction;
    private float speed;
    private float damage;
    [SerializeReference] private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y) + direction * speed * Time.deltaTime;
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
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
}
