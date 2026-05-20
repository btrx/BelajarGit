using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [Header("Pengaturan Pool")]
    public GameObject objectToPool;

    private List<GameObject> BulletPooll = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject GetPooledObject()
    {
        // Cari objek yang tidak aktif
        for (int i = 0; i < BulletPooll.Count; i++)
        {
            if (!BulletPooll[i].activeInHierarchy)
            {
                return BulletPooll[i];
            }
        }

        // Jika tidak ada, buat objek baru
        GameObject obj = Instantiate(objectToPool);

        obj.SetActive(false);

        BulletPooll.Add(obj);

        return obj;
    }
}