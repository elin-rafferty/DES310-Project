using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 10;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private ProjectileType bulletType;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer crosshair;
    [SerializeField] private EventHandler eventHandler;
    [SerializeField] private float maxHealth = 100;

    private bool inventoryOpen = false;
    private float health;


    // Start is called before the first frame update
    void Start()
    {
        eventHandler.InventoryChangeState.AddListener(InventoryStateChangeResponse);
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventoryOpen)
        {
            HandleInput();
        }
        Cursor.visible = inventoryOpen;
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
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        crosshair.transform.position = mouseWorldPos;
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

    void InventoryStateChangeResponse(bool open)
    {
        inventoryOpen = open;
    }

    public void Damage(float damage)
    {
        Debug.Log("I got attacked for " + damage + " damage! My health is now " + health);
        health -= damage;
        if (health < 0)
        {
            Debug.Log("Player died");
            SceneManager.LoadScene((SceneManager.GetActiveScene().name));
        }
    }
}
