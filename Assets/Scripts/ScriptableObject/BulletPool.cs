using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    public GameObject bulletPrefab;
    public int initialPoolSize = 20;

    // Maksimal ammo yang bisa dibawa
    public int maxAmmo = 20;
    // Ammo saat ini
    private int currentAmmo;
    // Lama waktu reload (detik)
    public float reloadTime = 2f;
    // Status sedang reload atau tidak
    private bool isReloading = false;

    private Queue<GameObject> availableBullets;
    private List<GameObject> allBullets;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        currentAmmo = maxAmmo;
        InitializePool();
    }

    void InitializePool()
    {
        availableBullets = new Queue<GameObject>(initialPoolSize);
        allBullets = new List<GameObject>(initialPoolSize);

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bullet.name = "Bullet_" + i;

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetPool(this);
            }

            availableBullets.Enqueue(bullet);
            allBullets.Add(bullet);
        }

        Debug.Log($"BulletPool initialized with {initialPoolSize} bullets");
    }

    public GameObject GetBullet(Vector3 position)
    {
        // Jika sedang reload, tidak bisa tembak
        if (isReloading)
        {
            Debug.Log("Sedang reload, tunggu!");
            return null;
        }

        // Jika ammo habis, mulai reload otomatis
        if (currentAmmo <= 0)
        {
            Debug.Log("Ammo habis! Auto reload...");
            StartCoroutine(Reload());
            return null;
        }

        // Jika pool kosong (semua peluru masih aktif di scene)
        if (availableBullets.Count == 0)
        {
            Debug.Log("Semua peluru masih aktif di scene!");
            return null;
        }

        // Kurangi ammo
        currentAmmo--;

        GameObject bullet = availableBullets.Dequeue();
        bullet.transform.position = position;
        bullet.SetActive(true);

        Debug.Log($"Ammo tersisa: {currentAmmo}/{maxAmmo}");
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        availableBullets.Enqueue(bullet);
    }

    // Dipanggil manual (tombol R) atau otomatis saat ammo habis
    public void StartReload()
    {
        if (!isReloading && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log($"Reloading... ({reloadTime} detik)");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log($"Reload selesai! Ammo: {currentAmmo}/{maxAmmo}");
    }

    // Getter untuk UI
    public int GetCurrentAmmo() => currentAmmo;
    public int GetMaxAmmo() => maxAmmo;
    public bool IsReloading() => isReloading;
    public int GetAvailableBulletsCount() => availableBullets.Count;
}