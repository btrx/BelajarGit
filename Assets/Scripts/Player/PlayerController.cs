using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections; // Dibutuhkan untuk IEnumerator / Coroutine

public class PlayerController : MonoBehaviour
{
    public float currentHP = 100;
    public float speed = 5f;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    private PlayerInput playerInput;
    private Vector2 moveInput;

    private float attackInput;
    private float previousAttackInput;

    [Header("Ammo & Reload System")]
    public int maxAmmo = 10;          // Kapasitas maksimal magazine
    private int currentAmmo;         // Jumlah peluru saat ini
    public float reloadTime = 2f;    // Waktu yang dibutuhkan untuk reload (detik)
    private bool isReloading = false; // Status apakah sedang reload atau tidak

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        currentAmmo = maxAmmo; // Isi penuh peluru di awal game
    }

    void Update()
    {
        if (playerInput == null) return;

        // --- 1. LOGIKA GERAKAN PLAYER ---
        // Player tetap bisa bergerak kapan saja, bahkan saat melakukan reload
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);

        // --- 2. LOGIKA CEK RELOAD ---
        // Jika sedang reload, hentikan pembacaan input menembak di bawahnya
        if (isReloading) return;

        // Deteksi input Reload (menekan tombol R pada keyboard atau jika peluru habis)
        if ((Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo) || currentAmmo <= 0)
        {
            StartCoroutine(ReloadRoutine());
            return;
        }

        // --- 3. LOGIKA MENEMBAK ---
        attackInput = playerInput.actions["Attack"].ReadValue<float>();

        // Cek apakah tombol serang baru saja ditekan
        if (previousAttackInput == 0 && attackInput > 0)
        {
            Shoot();
        }

        previousAttackInput = attackInput;
    }

    void Shoot()
    {
        // Validasi ketersediaan peluru sebelum menembak
        if (currentAmmo <= 0) return;

        Debug.Log("Player is shooting!");

        if (BulletPool.Instance == null)
        {
            Debug.LogError("BulletPool instance not found!");
            return;
        }

        if (BulletPool.Instance.bulletPrefab == null)
        {
            Debug.LogWarning("BulletPool has no bullet prefab assigned!");
        }

        // Kurangi jumlah peluru
        currentAmmo--;
        Debug.Log($"Peluru tersisa: {currentAmmo} / {maxAmmo}");

        // Tentukan posisi spawn peluru
        Vector3 spawnPos = bulletSpawnPoint != null ? bulletSpawnPoint.position : transform.position;

        // Ambil posisi mouse di world space (2D)
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;

        // Hitung arah tembakan dari posisi player ke arah mouse
        Vector3 shootDirection = (mouseWorldPos - spawnPos).normalized;

        // Mengambil peluru dari pool
        GameObject bulletObj = BulletPool.Instance.GetBullet(spawnPos);

        if (bulletObj != null)
        {
            bulletObj.transform.position = spawnPos;
            bulletObj.transform.rotation = Quaternion.identity;
            bulletObj.SetActive(true);

            // Berikan arah gerakan ke komponen Bullet
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.SetDirection(shootDirection);
            }
            else
            {
                Debug.LogError("Bullet component not found on prefab!");
            }
        }
    }

    // Coroutine untuk menangani jeda waktu reload
    IEnumerator ReloadRoutine()
    {
        isReloading = true;
        Debug.Log("Sedang Mengisi Ulang Peluru (Reload)...");

        // Menunggu selama durasi reloadTime
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Reload Selesai! Peluru kembali penuh.");
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(0.1f);
        }
    }

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}