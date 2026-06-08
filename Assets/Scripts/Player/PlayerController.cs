using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    public PlayerData playerData;

    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Camera mainCamera;
    private bool useAutoCameraFollow;
    private Vector2 moveInput;
    private Vector2 moveDirection;
    private float currentHP;
    private float speed;

    public GameObject peluruPrefab;
    public Transform peluruSpawnPoint;
    
    private void Awake()
    {
        Debug.Log("=== Player Awake START ===");
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        Debug.Log("playerInput: " + (playerInput != null ? "✓ Found" : "✗ NOT FOUND"));
        Debug.Log("rb: " + (rb != null ? "✓ Found" : "✗ NOT FOUND"));
        Debug.Log("mainCamera: " + (mainCamera != null ? "✓ Found" : "✗ NOT FOUND"));

        if (mainCamera == null)
        {
            GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
            if (camObj != null)
            {
                mainCamera = camObj.GetComponent<Camera>();
                Debug.Log("Found MainCamera by tag");
            }
        }

        if (mainCamera != null)
        {
            CameraFollow follow = mainCamera.GetComponent<CameraFollow>();
            if (follow == null)
            {
                mainCamera.gameObject.AddComponent<CameraFollow>();
                Debug.Log("✓ Added CameraFollow to Main Camera");
            }
        }
        else
        {
            Debug.LogError("✗ Main Camera NOT FOUND - player will likely disappear!");
        }

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.freezeRotation = true;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            Debug.Log("✓ Rigidbody2D configured: gravity=0, rotation frozen");
        }
        Debug.Log("=== Player Awake END ===");
    }

    private void Start()
    {
        Debug.Log("=== Player Start BEGIN ===");
        if (playerInput == null)
        {
            Debug.LogError("✗ PlayerInput is NULL in Start!");
        }
        else
        {
            Debug.Log("✓ PlayerInput exists");
        }

        if (playerData == null)
        {
            Debug.LogError("✗ PlayerData belum di-assign di Inspector!");
            currentHP = 100f;
            speed = 5f;
        }
        else
        {
            currentHP = playerData.maxHP;
            speed = playerData.moveSpeed;
            Debug.Log("✓ PlayerData assigned: maxHP=" + currentHP + ", speed=" + speed);
        }
        Debug.Log("=== Player Start END ===");
    }

    private void Update()
    {
        if (playerInput == null)
        {
            Debug.LogError("✗ PlayerInput is NULL in Update - CRITICAL!");
            return;
        }

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        moveDirection = moveInput.normalized;

        if (rb == null)
        {
            Vector3 movement = new Vector3(moveDirection.x, moveDirection.y, 0f) * speed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
                if (camObj != null)
                {
                    mainCamera = camObj.GetComponent<Camera>();
                    if (mainCamera != null && mainCamera.GetComponent<CameraFollow>() == null)
                    {
                        mainCamera.gameObject.AddComponent<CameraFollow>();
                        Debug.Log("Added CameraFollow in Update");
                    }
                }
            }
        }

        // Handle shooting input: prefer Input System action named "Shoot", fallback to left mouse button
        var shootAction = playerInput.actions.FindAction("Shoot", false);
        if (shootAction != null)
        {
            if (shootAction.triggered)
                Shoot();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
                Shoot();
        }
    }


    private void FixedUpdate()
    {
        if (rb != null)
        {
            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (playerData == null) return;
        if (collision.gameObject.CompareTag("Wall"))
        {
            ApplyDamage(playerData.wallDamagePerSecond * Time.fixedDeltaTime);
        }
    }

    private void ApplyDamage(float damage)
    {
        currentHP -= damage;
        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0f)
        {
            currentHP = 0f;
            Debug.Log("Player died: health reached 0");
            GameManager.Instance?.GameOver();
        }
    }

    // Public API so other objects (hazards, traps) can apply damage to the player
    public void TakeDamage(float damage)
    {
        ApplyDamage(damage);
    }

    private void OnEnable()
    {
        Debug.Log("=== Player OnEnable ===");
        Debug.Log("Active: " + gameObject.activeSelf + " | Position: " + transform.position + " | Tag: " + gameObject.tag);
    }

    private void OnDisable()
    {
        Debug.LogError("=== ⚠️ PLAYER OnDisable - DISAPPEARING! ===");
        Debug.LogError("Was active: " + gameObject.activeSelf + " | Pos: " + transform.position + " | Tag: " + gameObject.tag);
        Debug.LogError("rb exists: " + (rb != null) + " | playerInput exists: " + (playerInput != null));
        Debug.LogError("\n===== STACK TRACE =====\n" + System.Environment.StackTrace + "\n======================");
    }

    private void OnDestroy()
    {
        Debug.LogError("=== Player OnDestroy CALLED ===");
        Debug.LogError("Stack trace:\n" + System.Environment.StackTrace);
    }

    private void OnBecameInvisible()
    {
        Debug.LogWarning("⚠️ Player OnBecameInvisible - may be outside camera view");
        Debug.Log("Position: " + transform.position + " | Camera pos: " + (mainCamera != null ? mainCamera.transform.position.ToString() : "NO CAMERA"));
    }

    private void OnBecameVisible()
    {
        Debug.Log("✓ Player OnBecameVisible - back in view");
    }

    void Shoot()
    {
        Debug.Log("Player is shooting!");
        
        if (peluruPrefab == null)
        {
            Debug.LogWarning("Bullet prefab not assigned!");
            return;
        }

        // Determine spawn position
        Vector3 spawnPos = peluruSpawnPoint != null ? peluruSpawnPoint.position : transform.position;

        // Get mouse position in world space for 2D
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0; // Ensure Z is 0 for 2D
        
        // Calculate direction from player to mouse
        Vector3 shootDirection = (mouseWorldPos - spawnPos).normalized;
        
        Debug.Log($"Spawn Pos: {spawnPos}, Mouse World Pos: {mouseWorldPos}, Direction: {shootDirection}");

        // Instantiate bullet
        GameObject bulletObj = Instantiate(peluruPrefab, spawnPos, Quaternion.identity);
        
        // GameObject bulletObj = PooledObjects.Instance.GetPooledObject();

        if (bulletObj != null)
        {
            bulletObj.transform.position = spawnPos;
            bulletObj.transform.rotation = Quaternion.identity;
            bulletObj.SetActive(true);

            // Set bullet direction
            peluru bullet = bulletObj.GetComponent<peluru>();

            if (bullet != null)
            {
                bullet.SetDirection(shootDirection);
                Debug.Log($"Bullet direction set to: {shootDirection}");
            }
            else
            {
                Debug.LogError("Bullet component not found on prefab!");
            }
            Debug.Log("Bullet spawned!");
        }
    }
} 