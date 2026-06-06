using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public PlayerData data; 
    
    private PlayerInput playerInput;
    private Vector2 moveInput;
    private float currentHP;

    //fitur serangan

    // Variabel untuk menyimpan input serangan sebelumnya
    public GameObject bulletPrefab;
    // variabel untuk menentukan posisi spawn peluru
    public Transform bulletSpawnPoint;
    // variabel untuk melakukan serangan
    private float attackInput;
    // variabel untuk menyimpan input serangan sebelumnya agar bisa mendekati perubahan
    private float previousAttackInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
        if (data != null) 
        {
            currentHP = data.maxHP;
        }
    }

    void Update()
    {
        // Pastikan game tidak lagi dipause dan data tersedia   
        if (playerInput == null || data == null || Time.timeScale == 0) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        // baca input serangan 
        attackInput = playerInput.actions["Attack"].ReadValue<float>();
        
        // Logika Gerak: X untuk kanan/kiri, Y untuk atas/bawah
        Vector3 moveDirection = new Vector3(moveInput.x, moveInput.y, 0);
        transform.Translate(moveDirection * data.moveSpeed * Time.deltaTime);

        // ini untuk ngecek apakah tombol serang baru saja ditekan 
        if(previousAttackInput == 0 && attackInput > 0)
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

        // Instantiate bullet
        // GameObject bulletObj = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        
        GameObject bulletObj = PooledObjects.Instance.GetPooledObject();

        if (bulletObj != null)
        {
            bulletObj.transform.position = spawnPos;
            bulletObj.transform.rotation = Quaternion.identity;
            bulletObj.SetActive(true);

            // Set bullet direction
            Bullet bullet = bulletObj.GetComponent<Bullet>();

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

    void OnCollisionStay2D(Collision2D collision)
    {
        // Pastikan object penghalang punya Tag "Wall"
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(0.1f);
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}