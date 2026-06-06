using System.Collections.Generic;
using UnityEngine;

public class PooledObjects : MonoBehaviour
{
    public static PooledObjects Instance;

    [Tooltip("Default prefab used when no prefab is passed to GetPooledObject.")]
    public GameObject defaultPrefab;

    [Tooltip("Initial number of instances to create for each prefab pool.")]
    public int initialPoolSize = 20;

    private readonly Dictionary<GameObject, List<GameObject>> pools = new Dictionary<GameObject, List<GameObject>>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetPooledObject(GameObject prefab = null)
    {
        if (prefab == null)
            prefab = defaultPrefab;

        if (prefab == null)
            return null;

        if (!pools.TryGetValue(prefab, out var pool))
        {
            pool = new List<GameObject>();
            pools[prefab] = pool;
            InitializePool(prefab, pool, initialPoolSize);
        }

        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        var newObj = Instantiate(prefab);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }

    private void InitializePool(GameObject prefab, List<GameObject> pool, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }
}
