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
    [SerializeField] private GameObject crosshair;
    [SerializeField] private EventHandler eventHandler;
    [SerializeField] private InputManager inputManager;


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
        inputManager.UpdateKeys();
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
        // Get joystick input
        Vector2 joystickDirection = new Vector2(Input.GetAxis("Look X"), Input.GetAxis("Look Y"));
        Vector2 mouseDirection;
        Vector3 mouseWorldPos = Vector3.zero;
        crosshair = GetComponent<CrosshairManager>().crosshair;
        if (joystickDirection != Vector2.zero)
        {
            mouseDirection = joystickDirection.normalized * 3;
            mouseWorldPos = gameObject.transform.position + new Vector3(mouseDirection.x, mouseDirection.y, 0);
        }
        else
        {
            // Get mouse direction 
            Vector2 mousePos = Input.mousePosition;
            Vector2 screenMiddle = new Vector2(Screen.width / 2, Screen.height / 2);
            mouseDirection = mousePos - screenMiddle;
            mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            mouseDirection.Normalize();
        }
        crosshair.transform.position = mouseWorldPos;
        transform.rotation = Quaternion.identity;
        transform.Rotate(new Vector3(0, 0, 1), Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg);
        // Check if firing
        if (inputManager.GetButtonDown("Fire1"))
        {
            Fire(mouseDirection);
        }
        if (inputManager.GetButtonDown("Dash"))
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
        Destroy(newProjectile.gameObject, 0.35f);
    }


}
