using UnityEngine;
using System.Collections.Generic;

public class PooledObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static PooledObject Instance; 

    [Header("Pengaturan Pool")]
    public GameObject objectToPool;
    
    private List<GameObject> pooledObject = new List<GameObject>();

    void Awake() 
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    public GameObject GetPooledObject()
    {
        // Cari peluru yang sedang tidak aktif di Hierarchy
        for (int i = 0; i < pooledObject.Count; i++)
        {
            if (!pooledObject[i].activeInHierarchy)
            {
                return pooledObject[i]; 
            }
        }

        // Jika semua peluru sedang melayang (aktif), buat peluru ekstra
        GameObject obj = Instantiate(objectToPool);
        obj.SetActive(false); 
        pooledObject.Add(obj); 
        
        return obj; 
    }

}