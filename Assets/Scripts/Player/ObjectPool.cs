using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [Header("Pengaturan Pool")]
    public GameObject objectToPool;

    private List<GameObject> objectsPool = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < objectsPool.Count; i++)
        {
            if (!objectsPool[i].activeInHierarchy)
                return objectsPool[i];
        }

        // Semua aktif, buat baru
        GameObject obj = Instantiate(objectToPool);
        obj.SetActive(false);
        objectsPool.Add(obj);
        return obj;
    }
}