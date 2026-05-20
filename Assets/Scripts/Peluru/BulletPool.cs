using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    public GameObject bulletPrefab;
    public int initialPoolSize = 20;

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
        GameObject bullet;

        // Kalau masih ada bullet di pool
        if (availableBullets.Count > 0)
        {
            bullet = availableBullets.Dequeue();
        }
        else
        {
            // Kalau habis, jangan bikin bullet baru
            Debug.Log("No bullets available!");
            return null;
        }

        bullet.transform.position = position;
        bullet.SetActive(true);

        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        availableBullets.Enqueue(bullet);
    }

    public int GetAvailableBulletsCount()
    {
        return availableBullets.Count;
    }
}