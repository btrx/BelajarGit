using System.Collections.Generic;
using UnityEngine;

public class PooledObjects : MonoBehaviour
{
    public static PooledObjects Instance; 

    [Header("Pengaturan Pool")]
    public GameObject objectToPool;
    public int initialSize = 10;
    
    private List<GameObject> pooledObjects = new List<GameObject>();

    void Awake() 
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(objectToPool, transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i]; 
            }
        }

        GameObject obj = Instantiate(objectToPool, transform);
        obj.SetActive(false); 
        pooledObjects.Add(obj); 
        return obj; 
    }


    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}