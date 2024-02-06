using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 10;
    [SerializeField] private float dashStrength = 30;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private ProjectileType bulletType;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer crosshair;
    [SerializeField] private EventHandler eventHandler;


    private bool inventoryOpen = false;
    private float dashTime = 0;



    // Start is called before the first frame update
    void Start()
    {
        eventHandler.InventoryChangeState.AddListener(InventoryStateChangeResponse);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventoryOpen)
        {
            HandleInput();
        } else
        {
            rb.velocity = Vector3.zero;
        }
        Cursor.visible = inventoryOpen;
    }

    void HandleInput()
    {
        // Handle movement
        Vector2 velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        velocity *= movementSpeed;
        if (dashTime == 0)
        {
            rb.velocity = velocity;
        } else
        {
            dashTime -= Time.deltaTime;
            if (dashTime < 0)
            {
                dashTime = 0;
            }
        }
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
            Fire(mouseDirection);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash(velocity);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            gameObject.GetComponent<Health>().Damage(10);
        }
    }

    void InventoryStateChangeResponse(bool open)
    {
        inventoryOpen = open;
    }

    void Dash(Vector2 inputDir)
    {
        dashTime = 0.5f;
        Vector2 direction = inputDir.normalized;
        rb.velocity = direction * dashStrength;
    }

    void Fire(Vector2 mouseDirection)
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
