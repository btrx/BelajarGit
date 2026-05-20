using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData PlayerData;
    private float currentHP;
    private float speed;
    // Variabel untuk menyimpan input serangan sebelumnya
    public GameObject bulletPrefab;
    // variabel untuk menentukan posisi spawn peluru
    public Transform bulletSpawnPoint;
    private PlayerInput playerInput;
    private Vector2 moveInput;
      // Variabel untuk melakukan serangan
    private float AttackInput;
     // variabe untuk menyimpan input serangan sebelumnya agar bisa mendeteksi perubahan dari tidak menekan ke menekan
    private float previousAttackInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        currentHP = PlayerData.maxHP;
        speed = PlayerData.moveSpeed;
    }
    
    
    void Update()
    {
        if (playerInput == null) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        // Baca input serangan
        AttackInput = playerInput.actions["Attack"].ReadValue<float>();
        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
        if(previousAttackInput == 0 && AttackInput > 0)
        {
            Shoot();
        }
    previousAttackInput = AttackInput;

        // if (attackInput > 0)
        // {
        //     Shoot();
        // }
    }
    
    void Shoot()
    {
        Debug.Log("Player is shooting!");
        
        if (bulletPrefab == null)
        {
            Debug.LogWarning("BulletPool not found in scene!");
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
        GameObject bulletObj = BulletPool.Instance.GetBullet(spawnPos);
        if (bulletObj == null)
        {
            return;
        }
        
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

         // Tampilkan berapa banyak peluru yang masih tersedia di pool (newly added)
        Debug.Log($"Bullet spawned Pool has {BulletPool.Instance.GetAvailableBulletsCount()} bullets available.");
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