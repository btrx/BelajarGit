using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject bulletPrefab;
    public int poolSize = 10;

    private GameObject[] bullets;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        bullets = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            bullets[i] = Instantiate(bulletPrefab);
            bullets[i].SetActive(false);
        }
    }


    public GameObject GetBullet()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                return bullets[i];
            }
        }
        return null;
    }
}