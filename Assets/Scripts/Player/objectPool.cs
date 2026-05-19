using UnityEngine;
using System.Collections.Generic;

public class objectPool : MonoBehaviour
{
    public static objectPool Instance; 

    [Header("Pool Setting")]
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

        GameObject obj = Instantiate(objectToPool);
        obj.SetActive(false); 
        pooledObjects.Add(obj); 
        
        return obj; 

    }
}
