using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   // Patokan baru: Referensi ke Scriptable Object sesuai instruksi UTS
    public PlayerData data; 

    // Variabel tetap ada sesuai patokan awal kamu, tapi nilainya diambil dari 'data'
    public float currentHP; 
    public float speed;
    
    private PlayerInput playerInput;
    private Vector2 moveInput;

// variable untuk minyimpan input serangan sebelumnya
    public GameObject bulletPrefab;
// untuk menentukan posisi spawn peluru
    public Transform bulletSpawnPoint;
// untuk melakukan serangan
    private float attackInput;
// variabel untuk menyimpan input serangan sebelumnya agar bisa mendeteksi perubahan input
    private float previousAttackInput;


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        if (data != null)
        {
            currentHP = data.maxHP;
            speed = data.moveSpeed;
        }
        else
        {
            Debug.LogWarning("PlayerData Scriptable Object belum diassign! Pastikan untuk mengisi data pada Inspector.");
        }
    }
    
    
    void Update()
    {
        if (playerInput == null) return;
        
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        attackInput = playerInput.actions["Attack"].ReadValue<float>();// baru
       

        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
        // untuk mengecek apakah tombol serang baru saja ditekan (dari 0 ke 1)
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

        // Instantiate bullet
        //GameObject bulletObj = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

        
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