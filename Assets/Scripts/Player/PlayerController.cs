using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private float currentHP;
    private PlayerInput playerInput;
    private Vector2 moveInput;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private float attackInput;
    private float previousAttackInput;

    [Header("Magazine Settings")]
    public int maxAmmo = 10;
    private int currentAmmo;
    private bool isReloading = false;
    public float reloadTime = 2f;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerData == null)
        {
            Debug.LogError("PlayerData belum di-assign!");
            return;
        }

        currentHP = playerData.maxHP;
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (playerInput == null) return;

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        attackInput = playerInput.actions["Attack"].ReadValue<float>();

        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * playerData.moveSpeed * Time.deltaTime);

        if (previousAttackInput == 0 && attackInput > 0)
        {
            if (isReloading)
            {
                Debug.Log("Sedang reload, tunggu!");
            }
            else if (currentAmmo <= 0)
            {
                Debug.Log("Ammo habis! Auto reload...");
                StartCoroutine(Reload());
            }
            else
            {
                Shoot();
            }
        }

        previousAttackInput = attackInput;
    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log($"Reloading... ({reloadTime} detik)");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log($"Reload selesai! Ammo: {currentAmmo}/{maxAmmo}");
    }

    void Shoot()
    {
        Debug.Log("Player is shooting!");

        // Determine spawn position
        Vector3 spawnPos = bulletSpawnPoint != null ? bulletSpawnPoint.position : transform.position;

        // Get mouse position in world space for 2D
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;

        // Calculate direction from player to mouse
        Vector3 shootDirection = (mouseWorldPos - spawnPos).normalized;

        Debug.Log($"Spawn Pos: {spawnPos}, Mouse World Pos: {mouseWorldPos}, Direction: {shootDirection}");

        // Ambil objek dari Pool
        GameObject bulletObj = ObjectPool.Instance.GetPooledObject();

        if (bulletObj != null)
        {
            bulletObj.transform.position = spawnPos;
            bulletObj.transform.rotation = Quaternion.identity;
            bulletObj.SetActive(true);

            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.SetDirection(shootDirection);
                Debug.Log($"Bullet direction set to: {shootDirection}");
            }
            else
            {
                Debug.LogError("Bullet component not found on pooled object!");
            }

            Debug.Log("Bullet spawned!");

            // Kurangi ammo setelah berhasil spawn
            currentAmmo--;
            Debug.Log($"Ammo: {currentAmmo}/{maxAmmo}");

            // Auto reload kalau habis
            if (currentAmmo <= 0)
            {
                Debug.Log("Ammo habis! Auto reload...");
                StartCoroutine(Reload());
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(playerData.damagePerSecond * Time.deltaTime);
        }
    }

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        currentHP = Mathf.Max(0, currentHP);

        Debug.Log("HP: " + currentHP);

        if (currentHP <= 0)
        {
            Debug.Log("=== GAME OVER ===");
            GameManager.Instance.GameOver();
        }
    }
}