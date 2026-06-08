using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] private GameObject pooledPrefab;
    [SerializeField] private int poolSize = 20;

    private List<GameObject> pooledObjects = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Menyiapkan peluru di awal game (dalam keadaan mati)
        for (int i = 0; i < poolSize; i++)
        {
            // PERBAIKAN: Hapus tanda kurung setelah pooledPrefab agar tidak menjadi child
            GameObject obj = Instantiate(pooledPrefab); 
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // Fungsi untuk mengambil peluru yang sedang menganggur
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        
        // Opsi cadangan jika peluru di pool habis, buat baru
        // PERBAIKAN: Hapus tanda kurung setelah pooledPrefab agar tidak menjadi child
        GameObject obj = Instantiate(pooledPrefab);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
}