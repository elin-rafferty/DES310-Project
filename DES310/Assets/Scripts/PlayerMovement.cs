using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 10;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private ProjectileType bulletType;
    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Handle movement
        Vector2 velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        velocity *= movementSpeed;
        rb.velocity = velocity;
        // Get mouse direction
        Vector2 mousePos = Input.mousePosition;
        Vector2 screenMiddle = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 mouseDirection = mousePos - screenMiddle;
        mouseDirection.Normalize();
        transform.rotation = Quaternion.identity;
        transform.Rotate(new Vector3(0, 0, 1), Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg);
        // Check if firing
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Fire
            Projectile newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            newProjectile.SetType(bulletType);
            newProjectile.SetDirection(mouseDirection);
            newProjectile.SetOwner(gameObject);
            // Set projectile to despawn after a certain time has elapsed
            Destroy(newProjectile.gameObject, 1);
        }
    }
}
