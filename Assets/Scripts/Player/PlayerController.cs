using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // [POIN 20] Slot untuk narik file PlayerData.asset dari Project ke Inspector
    public PlayerData data;

    private float currentHP;
    private float speed;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    //variable untuk menyimpan input serangan sebelumnya, agar tidak terus menerus menyerang saat tombol ditekan
    public GameObject bulletPrefab;
    //variable untuk menentukan posisi spawn peluru, bisa diatur di Inspector dengan menambahkan empty gameobject sebagai child dari player dan drag ke slot ini
    public Transform bulletSpawnPoint;
    //varialbe input menyerang, bisa diatur di Input Actions dengan nama "Attack" dan tipe Button, lalu dipanggil di script ini untuk mendeteksi saat tombol serang ditekan
    private float attackInput;
    //variable untuk menyimpan input serangan yang sebelumnya agar bisa mendeteksi perubahan
    private float previousAttackInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        // Mengambil data dari Scriptable Object agar tidak 'hardcode'
        if (data != null)
        {
            currentHP = data.maxHP;      // Pakai 'maxHP' sesuai file PlayerData kamu
            speed = data.moveSpeed;    // Pakai 'moveSpeed' sesuai file PlayerData kamu
        }
        else
        {
            Debug.LogError("Nan, file PlayerData belum kamu masukin ke slot 'Data' di Inspector Player!");
        }
    }

    void Update()
    {
      // SEKARANG SUDAH DISESUAIKAN: pakai 'currentState' huruf c kecil sesuai script GameManager kamu
    if (GameManager.Instance != null && GameManager.Instance.currentState != GameState.Playing)
    {
        return; 
    }

    if (playerInput == null) return;

    moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

    // baca input serangan
    attackInput = playerInput.actions["Attack"].ReadValue<float>();

    float h = moveInput.x;
    float v = moveInput.y;

    // Sekarang 'speed' di sini nilainya otomatis ngikutin PlayerData
    transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
    
    // buat nge check apakah tombol serang baru saja ditekan, 
    if (previousAttackInput == 0 && attackInput > 0) // Deteksi perubahan dari tidak menyerang ke menyerang
    {
        Shoot();
    }
    previousAttackInput = attackInput; // Simpan input serangan saat ini untuk deteksi di frame berikutnya
    }

  void Shoot()
{
    Debug.Log("Player is shooting!");

    // Menentukan posisi spawn peluru
    Vector3 spawnPos = bulletSpawnPoint != null ? bulletSpawnPoint.position : transform.position;

    // Mengambil posisi mouse di screen untuk space 2D
    Vector3 mouseScreenPos = Input.mousePosition;
    mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);
    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
    mouseWorldPos.z = 0; // Pastikan Z bernilai 0 untuk 2D

    // Hitung arah dari player menuju posisi mouse
    Vector3 shootDirection = (mouseWorldPos - spawnPos).normalized;

    Debug.Log($"Spawn Pos: {spawnPos}, Mouse World Pos: {mouseWorldPos}, Direction: {shootDirection}");

    // MURNI AMBIL DARI POOL: Mengambil peluru pasif yang sudah standby di pool
    GameObject bulletObj = PooledObjects.Instance.GetPooledObject();

    if (bulletObj != null)
    {
        bulletObj.transform.position = spawnPos;
        bulletObj.transform.rotation = Quaternion.identity;
        bulletObj.SetActive(true); // Aktifkan peluru (OnEnable di Bullet.cs akan terpicu)

        // Atur arah terbang peluru
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.SetDirection(shootDirection);
            Debug.Log($"Bullet direction set to: {shootDirection}");
        }
        else
        {
            Debug.LogError("Komponen Bullet tidak ditemukan pada objek pool!");
        }
        Debug.Log("Bullet spawned dari Object Pool!");
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
            // Poin 20: Memanggil sistem GameOver saat darah habis
            GameManager.Instance.GameOver();
        }
    }
}