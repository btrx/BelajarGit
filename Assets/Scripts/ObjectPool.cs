using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [Header("Pool Settings")]
    public GameObject objectToPool;
    public int poolAmount = 20;

    private List<GameObject> pooledObjects = new List<GameObject>();

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 2. Kunci Perbaikan: Pindahkan pengisian bank peluru ke Awake agar siap LEBIH AWAL
        FillObjectPool();
    }

    void FillObjectPool()
    {
        if (objectToPool == null)
        {
            Debug.LogError("Object To Pool di Inspector masih kosong! Masukkan prefab peluru.");
            return;
        }

        for (int i = 0; i < poolAmount; i++)
        {
            // Perbaikan Utama: Tambahkan 'this.transform' agar klon peluru masuk ke dalam objek ObjectPool di Hierarchy
            GameObject obj = Instantiate(objectToPool, this.transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
        
        Debug.Log($"Bank peluru berhasil diisi dengan {poolAmount} peluru!");
    }

    // Fungsi untuk meminjam peluru dari bank
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // Cek jika peluru di bank sedang menganggur (tidak aktif)
            if (pooledObjects[i] != null && !pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        
       
        GameObject extraObj = Instantiate(objectToPool, this.transform);
        extraObj.SetActive(false);
        pooledObjects.Add(extraObj);
        return extraObj;
    }
}
