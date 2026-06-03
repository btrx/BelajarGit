using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float currentHP = 100;
    public float speed = 5f;
    // Variabel untuk menyimpan input serangan sebelumnya
    public GameObject bulletPrefab;
    // variabel untuk menentukan posisi spawn peluru
    public Transform bulletSpawnPoint;
    private PlayerInput playerInput;
    private Vector2 moveInput;
    // Variabel untuk melakukan serangan
    private float attackInput;
    // variabe untuk menyimpan input serangan sebelumnya agar bisa mendeteksi perubahan dari tidak menekan ke menekan
    private float previousAttackInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    
    
    void Update()
    {
        if (playerInput == null) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        // Baca input serangan
        attackInput = playerInput.actions["Attack"].ReadValue<float>();

        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
        
        // Ini untuk ngecek apakah tombol serang baru saja ditekan
        if (previousAttackInput == 0 && attackInput > 0)
        {
            Shoot();
        }
        
        previousAttackInput = attackInput;

        // if (attackInput > 0)
        // {
        //     Shoot();
        // }
    }

    void Shoot()
{
    Debug.Log("Player is shooting!");

    // 1. Validasi ObjectPool
    if (PooledObjects.Instance == null)
    {
        Debug.LogError("ObjectPool.Instance tidak ditemukan! Pastikan ada GameObject dengan script ObjectPool di scene.");
        return;
    }

    // 2. Tentukan posisi spawn
    Vector3 spawnPos = bulletSpawnPoint != null ? bulletSpawnPoint.position : transform.position;

    // 3. Validasi Camera.main
    if (Camera.main == null)
    {
        Debug.LogError("Camera.main tidak ditemukan! Pastikan kamera memiliki tag 'MainCamera'.");
        return;
    }

    // 4. Hitung arah ke mouse
    Vector3 mouseScreenPos = Input.mousePosition;
    mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
    mouseWorldPos.z = 0f;
    Vector3 shootDirection = (mouseWorldPos - spawnPos).normalized;

    // Debug.Log($"Spawn Pos: {spawnPos}, Mouse World Pos: {mouseWorldPos}, Direction: {shootDirection}");

    // 5. Ambil peluru dari pool (gunakan ObjectPool, bukan PooledObjects)
    GameObject bulletObj = PooledObjects.Instance.GetPooledObject();

    if (bulletObj != null)
    {
        bulletObj.transform.position = spawnPos;
        bulletObj.transform.rotation = Quaternion.identity;
        bulletObj.SetActive(true);

        // Set arah peluru
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetDirection(shootDirection);
            Debug.Log($"Bullet direction set to: {shootDirection}");
        }
        else
        {
            Debug.LogError("Komponen Bullet tidak ditemukan pada prefab peluru!");
        }

        Debug.Log("Bullet spawned!");
    }
    else
    {
        Debug.LogError("Gagal mendapatkan peluru dari pool!");
    }
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