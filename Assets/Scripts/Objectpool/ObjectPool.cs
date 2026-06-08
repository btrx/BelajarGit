using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [Header("Pengaturan Pool")]
    public GameObject objectToPool;

    private List<GameObject> objectPool = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject GetPooledObject()
    {
        // Cari object yang tidak aktif
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }

        // Jika tidak ada yang tersedia, buat object baru
        GameObject obj = Instantiate(objectToPool);
        obj.SetActive(false);
        objectPool.Add(obj);

        return obj;
    }
}