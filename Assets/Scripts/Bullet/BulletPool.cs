using UnityEngine;

public class BulletPool : MonoBehaviour
{
    // Instance statis untuk Singleton pattern (pola desain yang memastikan hanya ada satu instance)
    public static BulletPool Instance { get; private set; }

    // Prefab peluru yang akan di-clone
    public GameObject bulletPrefab;

    // Jumlah peluru yang akan dibuat di awal permainan
    public int initialPoolSize = 20;

    // Array untuk menyimpan semua peluru
    private GameObject[] bullets;

    // Index peluru saat ini
    private int currentBulletIndex = 0;

    void Awake()
    {
        // Pastikan hanya ada satu instance BulletPool di scene
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

            return;
        }

        // Atur instance statis ke objek ini
        Instance = this;
    }

    void Start()
    {
        // Inisialisasi pool saat game dimulai
        InitializePool();
    }

    void InitializePool()
    {
        // Buat array peluru
        bullets = new GameObject[initialPoolSize];

        // Buat peluru sebanyak initialPoolSize
        for (int i = 0; i < initialPoolSize; i++)
        {
            // Clone peluru dari prefab
            GameObject bullet = Instantiate(bulletPrefab);

            // Nonaktifkan peluru saat dibuat
            bullet.SetActive(false);

            // Berikan nama untuk debugging
            bullet.name = "Bullet_" + i;

            // Simpan ke array
            bullets[i] = bullet;
        }

        Debug.Log($"BulletPool initialized with {initialPoolSize} bullets");
    }

    public GameObject GetBullet(Vector3 position)
    {
        // Jika semua bullet sudah digunakan
        if (currentBulletIndex >= bullets.Length)
        {
            Debug.Log("No bullets left!");

            return null;
        }

        // Ambil bullet berdasarkan index
        GameObject bullet = bullets[currentBulletIndex];

        // Naikkan index
        currentBulletIndex++;

        // Atur posisi bullet
        bullet.transform.position = position;

        // Aktifkan bullet
        bullet.SetActive(true);

        return bullet;
    }

    public int GetRemainingBullets()
    {
        return bullets.Length - currentBulletIndex;
    }
}