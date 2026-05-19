using UnityEngine;
using System.Collections.Generic;

public class PooledObjects : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static PooledObjects Instance; 

    [Header("Pengaturan Pool")]
    public GameObject objectToPool;
    
    private List<GameObject> pooledObjects = new List<GameObject>();

    void Awake() 
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    public GameObject GetPooledObject()
    {
        // Cari peluru yang sedang tidak aktif di Hierarchy
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i]; 
            }
        }

        // Jika semua peluru sedang melayang (aktif), buat peluru ekstra
        GameObject obj = Instantiate(objectToPool);
        obj.SetActive(false); 
        pooledObjects.Add(obj); 
        
        return obj; 
    }

}
