using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour
{
    [Header("Data Settings")]
    [SerializeField] private PlayerData playerData; 

    [Header("Shooting Settings")]
    [SerializeField] private Transform bulletSpawnPoint;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private float currentHP; // Variabel penampung nyawa berjalan

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 🟢 YANG KURANG 1: Mengisi darah awal player saat game dimulai
    void Start()
    {
        if (playerData != null)
        {
            currentHP = playerData.maxHP; // Mengambil angka 10 dari PlayerData asset kamu
            Debug.Log($"Player HP Initialized: {currentHP}");
        }
        else
        {
            Debug.LogError("PlayerData asset belum dimasukkan ke slot di Inspector Player!");
        }
    }

  void Update()
    {
        // Deteksi klik kiri mouse untuk menembak
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // FITUR PAUSE: Sistem universal menggunakan Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0f; 
                Debug.Log("Game Paused!");
            }
            else
            {
                Time.timeScale = 1f; 
                Debug.Log("Game Resumed!");
            }
        }

        // 🟢 FITUR RESTART: Jika player sudah mati (HP <= 0) dan menekan tombol R
        if (currentHP <= 0 && Input.GetKeyDown(KeyCode.R))
        {
            // Kembalikan kecepatan waktu game menjadi normal (jika sebelumnya sempat membeku)
            Time.timeScale = 1f;
            
            // Mengulang scene yang sedang aktif saat ini dari awal
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("Game Restarted via 'R' Key!");
        }
    }
    void FixedUpdate()
    {
        // MENANGGANI PERGERAKAN PLAYER
        if (rb != null && playerData != null)
        {
            rb.linearVelocity = moveInput * playerData.moveSpeed;
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // 🟢 YANG KURANG 2: Fungsi untuk mengurangi darah saat ditabrak musuh
    public void TakeDamage(float damageAmount)
    {
        currentHP -= damageAmount;
        Debug.Log($"Player hit! Remaining HP: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player HP reached 0. Triggering Game Over...");
        
        // JANGAN pakai gameObject.SetActive(false); agar script ini tidak ikutan mati!
        // Sebagai gantinya, kita matikan fungsi visual dan pergerakannya saja:
        
        // 1. Matikan gambar/sprite player agar tidak kelihatan di layar
        if (GetComponent<SpriteRenderer>() != null) 
            GetComponent<SpriteRenderer>().enabled = false;

        // 2. Matikan Collider fisika agar tidak bisa ditabrak-tabrak lagi saat sudah mati
        if (GetComponent<Collider2D>() != null) 
            GetComponent<Collider2D>().enabled = false;

        // 3. Set kecepatan fisika menjadi nol agar langsung berhenti diam di tempat
        if (rb != null) 
            rb.linearVelocity = Vector2.zero;

        // Panggil fungsi Game Over bawaan template UAS kamu
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver(); 
        }
    }

    void Shoot()
    {
        Debug.Log("Player is shooting!");

        Vector3 spawnPos = bulletSpawnPoint != null ? bulletSpawnPoint.position : transform.position;

        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;
        
        Vector3 shootDirection = (mouseWorldPos - spawnPos).normalized;

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
            }
        }
    }
    // Fungsi bawaan Unity untuk mendeteksi tabrakan fisik keras (Collision)
   // Fungsi bawaan Unity untuk mendeteksi tabrakan fisik keras (Collision)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Mengecek apakah nama objek yang ditabrak mengandung kata "Tilemap", "Grid", atau "Corner"
        if (collision.gameObject.name.Contains("Tilemap") || 
            collision.gameObject.name.Contains("Grid") || 
            collision.gameObject.name.Contains("Corner"))
        {
            // Kurangi darah player sebanyak 1
            TakeDamage(1f);
            Debug.Log($"Player menabrak {collision.gameObject.name}! Darah berkurang.");
        } 
    } 

    // OPSI CADANGAN: Jika tembok merah kamu diatur sebagai "Is Trigger" (bisa ditembus)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Grid") || collision.gameObject.CompareTag("Finish"))
        {
            TakeDamage(1f);
            Debug.Log("Player menembus batas tembok merah! Darah berkurang.");
        }
    }
    
}