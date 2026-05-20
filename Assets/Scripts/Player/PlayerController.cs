using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;

    private float currentHP;
    private float speed;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    private PlayerInput playerInput;
    private Vector2 moveInput;
    private float attackInput;
    private float previousAttackInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        // Ambil data dari ScriptableObject (playerData) dan mengaplikasikan ke dalam player
        currentHP = playerData.maxHP; // 100% HP saat mulai
        speed = playerData.moveSpeed;
    }
    
    void Update()
    {
        if (playerInput == null) return;
        // membaca input keyboard untuk bergerak
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        float h = moveInput.x;
        float v = moveInput.y;

        attackInput = playerInput.actions["Attack"].ReadValue<float>();
        
        // h = nilai horizontal, v = nilai vertikal, 0 untuk sumbu z karena ini game 2D 
        // transform.translate untuk membaca input keyboarduntuk menggerakan player
        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);

        if (previousAttackInput == 0 && attackInput > 0)
        {
            Shoot();
        }
        
        previousAttackInput = attackInput;
    }
    
    void Shoot()
    {
        Debug.Log("Player is shooting!");
        
        if (bulletPrefab == null)
        {
            Debug.LogWarning("Bullet prefab not assigned!");
            return;
        }
        // Determine spawn position
        Vector3 spawnPos = bulletSpawnPoint != null ? bulletSpawnPoint.position : transform.position;

        // Get mouse position in world space for 2D
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0; // Ensure Z is 0 for 2D
        
        // Calculate direction from player to mouse
        Vector3 shootDirection = (mouseWorldPos - spawnPos).normalized;
        
        Debug.Log($"Spawn Pos: {spawnPos}, Mouse World Pos: {mouseWorldPos}, Direction: {shootDirection}");

        // menggunakan object pooling untuk mendapatkan peluru dari pool yang sudah dibuat sebelumnya 
        GameObject bulletshot = BulletPool.Instance.GetBullet(spawnPos);

        if (bulletshot != null)
        {
            // Atur posisi dan rotasi peluru
            bulletshot.transform.position = spawnPos;
            bulletshot.transform.rotation = Quaternion.identity;
            
            // Aktifkan peluru
            bulletshot.SetActive(true);

            // Set arah peluru (Logika aslimu tetap dipertahankan)
            Bullet bullet = bulletshot.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.SetDirection(shootDirection);
                Debug.Log($"Bullet direction set to: {shootDirection}");
            }
            else
            {
                Debug.LogError("Bullet component not found on prefab!");
            }
        }
        
        Debug.Log("Bullet spawned!");
    }

    // mendeteksi jika player bertabrakan dengan dinding, jika iya maka player akan menerima damage sebesar 0.5 HP per detik
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(0.5f);
        }
    }

    void HandleMovement()
    {
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        
        float h = moveInput.x;
        float v = moveInput.y;

        Vector3 direction = new Vector3(h, v, 0);
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // Fungsi untuk menerima damage dan mengurangi HP player, jika HP mencapai 0 maka game over
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