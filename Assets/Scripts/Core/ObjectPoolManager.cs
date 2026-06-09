using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager instance;

    public static ObjectPoolManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindFirstObjectByType<ObjectPoolManager>();
            return instance;
        }
    }

    [Header("Pool Settings")]
    public GameObject bulletPrefab;
    public BulletData bulletData;
    public int initialPoolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        int size = bulletData != null ? bulletData.poolSize : initialPoolSize;
        for (int i = 0; i < size; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false);
            pool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (pool.Count > 0)
        {
            GameObject bullet = pool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(true);
            return bullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        pool.Enqueue(bullet);
    }

    public int AvailableCount => pool.Count;
}
