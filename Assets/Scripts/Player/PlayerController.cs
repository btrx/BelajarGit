using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Data karakter pemain yang berisi nilai-nilai seperti maxHP dan moveSpeed.
    public PlayerData playerData;
    // Slider UI untuk menampilkan HP pemain.
    public Slider hpSlider;
    // Text UI untuk menampilkan nilai HP dalam angka.
    public Text hpText;

    // HP saat ini dari pemain.
    private float currentHP;
    // Komponen Input System untuk membaca input pemain.
    private PlayerInput playerInput;
    // Input gerakan dari pemain, biasanya pakai keyboard atau joystick.
    private Vector2 moveInput;

    //variable untuk menyimpan input serangan sebelumnya
    public GameObject bulletPrefab;
    // variable untuk menentukan titik spawn peluru
    public Transform bulletSpawnPoint;
    // untuk melakukan serangan
    private float attackInput;
    //menyimpan input serangan
    private float previousAttackInput;

    void Start()
    {
        // Ambil komponen PlayerInput yang ada di GameObject ini.
        playerInput = GetComponent<PlayerInput>();
        // Set HP awal sesuai dengan maxHP yang tercantum di PlayerData.
        currentHP = playerData.maxHP;

        if (hpSlider != null)
        {
            // Atur nilai maksimum slider sesuai dengan HP maksimum.
            hpSlider.maxValue = playerData.maxHP;
        }

        // Tampilkan nilai HP awal di UI.
       
    }
    
    
    void Update()
    {
        if (playerInput == null) return;
        
        // Baca nilai input gerakan dari action "Move".
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        float h = moveInput.x;
        float v = moveInput.y;

        // Pindahkan objek pemain berdasarkan input dan kecepatan.
        transform.Translate(new Vector3(h, v, 0) * playerData.moveSpeed * Time.deltaTime);
    
        // Baca input serangan
        attackInput = playerInput.actions["Attack"].ReadValue<float>();

        //ini untuk mengecek apakah tombol serang baru saja ditekan
        if (previousAttackInput == 0 && attackInput > 0)
        {
            Shoot();
        }
        previousAttackInput = attackInput;
    }
    // method menembak
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

      
        GameObject bulletObj = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        
        
    void OnCollisionStay2D(Collision2D collision)
    {
        // Jika pemain terus menempel pada objek bertag "Wall", ambil damage.
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(0.1f);
        }
    }

    void TakeDamage(float dmg)
    {
        // Kurangi HP, tetapi jangan sampai kurang dari 0.
        currentHP = Mathf.Max(currentHP - dmg, 0f);
        Debug.Log("Player HP: " + currentHP);

        // Update tampilan HP di UI.
        UpdateHPUI();

        // Jika HP habis, panggil game over.
        if (currentHP <= 0f)
        {
            GameManager.Instance.GameOver();
        }
    }

    void UpdateHPUI()
    {
        if (hpSlider != null)
        {
            // Set posisi slider ke nilai HP saat ini.
            hpSlider.value = currentHP;
        }

        if (hpText != null)
        {
            // Tampilkan teks HP saat ini dan HP maksimum.
            hpText.text = $"HP: {currentHP:0}/{playerData.maxHP:0}";
        }
    }
}
}