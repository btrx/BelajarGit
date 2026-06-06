using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public PlayerData data;
    public TextMeshProUGUI hpText; // Tarik objek Text HP kamu ke sini di Inspector
    private float currentHP;
    private PlayerInput playerInput;

    public GameObject bulletPrefab; // Prefab peluru
    public Transform bulletSpawnPoint; // Titik tembak peluru
    private float attackInput; // Variabel untuk menyimpan input serangan
    private float previousAttackInput; // Variabel untuk menyimpan input serangan sebelumnya


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
        if (data != null) 
        {
            currentHP = data.maxHP;
            UpdateHpUI(); // Update tampilan HP saat start
        }
    }

    void Update()
    {
        // 1. CEK STATE & NULL: Keamanan agar tidak error saat pindah scene
        if (GameManager.Instance == null || GameManager.Instance.currentState != GameState.Playing) 
            return;
            
        if (playerInput == null || data == null) return;

        // 2. LOGIKA GERAK
        Vector2 moveInput = Vector2.zero;
        var moveAction = playerInput.actions["Move"];
        if (moveAction != null)
        {
            moveInput = moveAction.ReadValue<Vector2>();
        }
        transform.Translate(new Vector3(moveInput.x, moveInput.y, 0) * data.moveSpeed * Time.deltaTime);

        // 3. LOGIKA SERANG
        var attackAction = playerInput.actions["Attack"];
        attackInput = 0f;
        if (attackAction != null)
        {
            attackInput = attackAction.ReadValue<float>();
        }

        if (previousAttackInput == 0 && attackInput > 0) // Deteksi transisi dari tidak menekan ke menekan tombol serang
        {
            Shoot();
        }
        previousAttackInput = attackInput;
    }

    void Shoot()
    {
        if (bulletPrefab == null)
            return;

        Vector3 spawnPos = bulletSpawnPoint != null
            ? bulletSpawnPoint.position
            : transform.position;

        if (Camera.main == null)
            return;

        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;

        Vector3 shootDirection = (mouseWorldPos - spawnPos).normalized;

        GameObject bulletObj = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

        if (bulletObj == null)
            return;

        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetDirection(shootDirection);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // 3. DETEKSI DAMAGE (Tembok)
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Menggunakan Time.deltaTime agar damage stabil di semua PC (frame rate independent)
            TakeDamage(15f * Time.deltaTime);
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        
        // Pastikan HP tidak minus di UI
        currentHP = Mathf.Max(0, currentHP);

        // Update Console
        Debug.Log("Player terkena damage! Sisa HP: " + currentHP.ToString("F0"));

        // Update UI Text
        UpdateHpUI();

        // 4. CEK MATI
        if (currentHP <= 0)
        {
            Debug.Log("<color=red>Player Mati!</color>");
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    void UpdateHpUI()
    {
        if (hpText != null)
        {
            // "F0" artinya tidak menampilkan angka di belakang koma agar rapi
            hpText.text = "HP: " + currentHP.ToString("F0");
        }
    }
}