using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Pathfinding.SimpleSmoothModifier;

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
    [SerializeField] private GameObject barrelEnd;
    [SerializeField] private Slider overheatSlider;
    [SerializeField] private float fireDelay = 0.1f;
    [SerializeField] private float dashCooldown = 1.5f;
    [SerializeField] private float overheatCapacity = 5;


    private bool inventoryOpen = false;
    private float dashTime = 0;
    private Vector2 lastAimPosition = new Vector2(1, 0);
    private float timeTilNextFire = 0;
    private float dashCooldownTimer = 0;
    private float overheatLevel;
    private bool overheated;

    private float rotateSpeed;
    private float smoothTime = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        eventHandler.InventoryChangeState.AddListener(InventoryStateChangeResponse);
        overheatSlider.value = 0;
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

    void FixedUpdate()
    {
        if (!inventoryOpen) 
        { 
            HandlePlayerMovement();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void HandleInput()
    {
        if (timeTilNextFire > 0)
        {
            timeTilNextFire -= Time.deltaTime;
            if (timeTilNextFire < 0)
            {
                timeTilNextFire = 0;
            }
        }
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer < 0)
            {
                dashCooldownTimer = 0;
            }
        }
        if (overheatLevel > 0)
        {
            if (overheated)
            {
                overheatLevel -= Time.deltaTime * 2;
            }
            overheatLevel -= Time.deltaTime;
            if (overheatLevel < 0)
            {
                overheatLevel = 0;
                overheated = false;
            }
            overheatSlider.value = overheatLevel;
        }
        //THIS IS SUPER TEMPORARY DELETE LATER PLZ
        if (Input.GetKeyDown(KeyCode.Tab) || inputManager.GetButtonDown("StartButton"))
        {
            SceneManager.LoadScene("Main Menu");
        }
        inputManager.UpdateKeys();
        
        crosshair = GetComponent<CrosshairManager>().crosshair;
        Vector2 lookDirection = lastAimPosition;
        if (true)
        {

                if (Input.GetAxis("Look X") != 0 || Input.GetAxis("Look Y") != 0)
                {
                    // Get joystick input
                    lookDirection = new Vector2(Input.GetAxis("Look X"), Input.GetAxis("Look Y"));
                    lookDirection.Normalize();
                }
                crosshair.transform.position = gameObject.transform.position + new Vector3(lookDirection.x * 3, lookDirection.y * 3, -1);
            }
        else
        {
            // Get mouse direction 
            Vector2 mousePos = Input.mousePosition;
            Vector2 screenMiddle = new Vector2(Screen.width / 2, Screen.height / 2);
            Vector2 mouseDirection = mousePos - screenMiddle;
            lookDirection = mouseDirection;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = -1;
            mouseDirection.Normalize();
            crosshair.transform.position = mouseWorldPos;
        }

        // calculate player angle of rotation
        float targetAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref rotateSpeed, smoothTime);

        // WHAT DOES THIS DO
        lastAimPosition = lookDirection;
        transform.rotation = Quaternion.identity;

        // Rotate player towards crosshair
        transform.Rotate(new Vector3(0, 0, 1), angle);
        // Old rotate code
        //transform.Rotate(new Vector3(0, 0, 1), Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg);

        // Check if firing
        if (inputManager.GetButton("Fire1") && timeTilNextFire == 0 && !overheated)
        {
            Fire(lookDirection);
        }
        // THIS IS ALSO SUPER TEMPORARY DELETE THIS LATER TOO
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
        dashTime = 0.15f;
        Vector2 direction = inputDir.normalized;
        rb.velocity = direction * dashStrength;
        dashCooldownTimer = dashCooldown;
    }

    void Fire(Vector2 mouseDirection)
    {
        // Fire
        Projectile newProjectile = Instantiate(projectilePrefab, barrelEnd.transform.position, Quaternion.identity);
        newProjectile.SetType(bulletType);
        newProjectile.SetDirection(mouseDirection);
        newProjectile.SetOwner(gameObject);
        newProjectile.transform.Rotate(0, 0, Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg);
        // Set projectile to despawn after a certain time has elapsed
        Destroy(newProjectile.gameObject, 0.35f);
        timeTilNextFire = fireDelay;
        overheatLevel += fireDelay * 2;
        overheatSlider.value = overheatLevel;
        if (overheatLevel >= overheatCapacity)
        {
            overheated = true;
        }

        // Play shoot sound
        SoundManager.instance.PlaySound(SoundManager.SFX.PlayerShoot, transform, 1f);
    }

    void HandlePlayerMovement()
    {
        // Handle directional movement
        Vector2 velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        velocity *= movementSpeed;
        if (dashTime == 0)
        {
            rb.velocity = velocity;
        }
        else
        {
            dashTime -= Time.deltaTime;
            if (dashTime < 0)
            {
                dashTime = 0;
            }
        }


        // Player Dash
        if (inputManager.GetButtonDown("Dash") && rb.velocity != Vector2.zero && dashCooldownTimer == 0)
        {
            Dash(velocity);
        }
    }
}
