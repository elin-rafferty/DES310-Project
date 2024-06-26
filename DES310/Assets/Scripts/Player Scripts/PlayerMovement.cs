using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 10;
    [SerializeField] private float buffedMovementSpeed = 20;
    [SerializeField] private float dashStrength = 30;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private ProjectileType bulletType;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private EventHandler eventHandler;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject barrelEnd;
    [SerializeField] private Slider overheatSlider;
    [SerializeField] private float dashCooldown = 1.5f;
    [SerializeField] private SettingsSO settings;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private OptionsMenu optionsMenu;
    [SerializeField] private GameObject bubble, backpackPos;
    [SerializeField] private ActiveBuffs activeBuffs;
    [SerializeField] private GameObject pistolMuzzleFlash, rifleMuzzleFlash, shotgunMuzzleFlash;


    private bool inventoryOpen = false;
    private float dashTime = 0;
    private Vector2 lastAimPosition = new Vector2(1, 0);
    private float timeTilNextFire = 0;
    private float dashCooldownTimer = 0;
    private float overheatLevel;
    private bool overheated;
    private float rotateSpeed;
    private float smoothTime = 0.05f;
    private AgentWeapon agentWeapon;
    private float bubbleTimer = 0.1f;
    private bool doBubbles = false;
    private float muzzleFlashTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        eventHandler.InventoryChangeState.AddListener(InventoryStateChangeResponse);
        agentWeapon = GetComponent<AgentWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleTimers();
        HandleUnconditionalInput();
        if (!inventoryOpen && !pauseMenu.activeSelf)
        {
            HandleInput();
        } 
        if (!pauseMenu.activeSelf)
        {
            Cursor.visible = inventoryOpen && settings.Controls == 0;
        }
        else if (pauseMenu.activeSelf)
        {
            Cursor.visible = settings.Controls == 0;
        }
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

    void HandleTimers()
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
                doBubbles = false;
                dashCooldownTimer = 0;
            }
        }
        if (overheatLevel > 0 && timeTilNextFire == 0)
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
            overheatSlider.value = overheatSlider.maxValue - overheatLevel;
        }
        if (bubbleTimer > 0)
        {
            bubbleTimer -= Time.deltaTime;
        }
        if (muzzleFlashTimer > 0)
        {
            muzzleFlashTimer -= Time.deltaTime;
            if (muzzleFlashTimer < 0)
            {
                muzzleFlashTimer = 0;
                pistolMuzzleFlash.SetActive(false);
                rifleMuzzleFlash.SetActive(false);
                shotgunMuzzleFlash.SetActive(false);
            }
        }
    }

    void HandleInput()
    {
        /*//THIS IS SUPER TEMPORARY DELETE LATER PLZ
        if (Input.GetKeyDown(KeyCode.M) || inputManager.GetButtonDown("StartButton"))
        {
            SceneManager.LoadScene("Main Menu");
        }*/

        crosshair = GetComponent<CrosshairManager>().crosshair;
        Vector2 lookDirection = lastAimPosition;
        if (settings.Controls == 1)
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
        }

        // calculate player angle of rotation
        float targetAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref rotateSpeed, smoothTime);

        lastAimPosition = lookDirection;
        transform.rotation = Quaternion.identity;

        // Rotate player towards crosshair
        transform.Rotate(new Vector3(0, 0, 1), angle);
        // Old rotate player code
        //transform.Rotate(new Vector3(0, 0, 1), Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg);

        // Check if firing
        bool fireInput = false;
        if (agentWeapon.weapon != null)
        {
            if (agentWeapon.weapon.weaponProperties.isSingleFire)
            {
                fireInput = inputManager.GetButtonDown("Fire1");
            } else
            {
                fireInput = inputManager.GetButton("Fire1");
            }
        }
        if (fireInput && timeTilNextFire == 0 && !overheated)
        {
            eventHandler.PlayerBulletFired.Invoke();
            Fire(lookDirection);
        }/*
        // THIS IS ALSO SUPER TEMPORARY DELETE THIS LATER TOO
        if (Input.GetKeyDown(KeyCode.H))
        {
            gameObject.GetComponent<Health>().Damage(10);
        }*/
        // Player Dash
        if (inputManager.GetButtonDown("Dash") && rb.velocity != Vector2.zero && dashCooldownTimer == 0)
        {
            doBubbles = true;
            Dash(rb.velocity);
        }
    }

    void HandleUnconditionalInput()
    {
    }

    void InventoryStateChangeResponse(bool open)
    {
        inventoryOpen = open;

        SoundManager.instance.PlaySound(SoundManager.SFX.InventoryOpen, transform, 0.3f);
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
        if (!pauseMenu.activeSelf)
        {
            if (GetComponent<AgentWeapon>().weapon != null)
            {
                WeaponProperties weaponProperties = GetComponent<AgentWeapon>().weapon.weaponProperties;
                weaponProperties.Fire(barrelEnd.transform.position, mouseDirection, this.gameObject);
                timeTilNextFire = weaponProperties.fireDelay / weaponProperties.weaponUpgrades.fireSpeedModifier;
                if (!activeBuffs.IsBuffActive(BuffType.NO_OVERHEAT))
                {
                    overheatLevel += weaponProperties.fireDelay / weaponProperties.weaponUpgrades.fireSpeedModifier;
                    overheatSlider.value = overheatSlider.maxValue - overheatLevel;
                    if (overheatLevel >= weaponProperties.overheatCapacity * weaponProperties.weaponUpgrades.overheatCapacityModifier)
                    {
                        overheated = true;
                        SoundManager.instance.PlaySound(SoundManager.SFX.Overheat, transform, 0.05f);
                    }
                }
                // Play shoot sound
                SoundManager.instance.PlaySound(weaponProperties.sound, transform, 0.3f);
                switch (GetComponent<AgentWeapon>().weapon.Name)
                {
                    case "Laser Pistol":
                        pistolMuzzleFlash.SetActive(true);
                        break;
                    case "Laser Rifle":
                        rifleMuzzleFlash.SetActive(true);
                        break;
                    case "Laser Shotgun":
                        shotgunMuzzleFlash.SetActive(true);
                        break;
                }
                muzzleFlashTimer = 0.1f;
            }
        }
    }

    void HandlePlayerMovement()
    {
        // Handle directional movement
        Vector2 velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (velocity.SqrMagnitude() > 1)
        {
            velocity.Normalize();
        }
        velocity *= activeBuffs.IsBuffActive(BuffType.SPEED_BOOST) ? buffedMovementSpeed : movementSpeed;
        if (bubbleTimer <= 0)
        {
            bubbleTimer = 0.25f;
            if (doBubbles)
            {
                Instantiate(bubble, backpackPos.gameObject.transform.position + new Vector3(UnityEngine.Random.Range(0, 0.5f), UnityEngine.Random.Range(0, 0.5f), 1), Quaternion.identity);
            }
        }
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
    }
}
