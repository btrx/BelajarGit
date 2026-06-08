using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGuardian : MonoBehaviour
{
    public float checkInterval = 0.5f;
    private float timer = 0f;
    private bool sceneLoading = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        sceneLoading = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // small delay before guardian acts to allow scene setup
        sceneLoading = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < checkInterval) return;
        timer = 0f;

        if (sceneLoading) return;

        var activeScene = SceneManager.GetActiveScene();
        var roots = activeScene.GetRootGameObjects();
        GameObject foundPlayer = null;
        foreach (var root in roots)
        {
            if (root == null) continue;
            // check root and children even if inactive
            if (IsPlayerObject(root)) { foundPlayer = root; break; }
            var transforms = root.GetComponentsInChildren<Transform>(true);
            foreach (var t in transforms)
            {
                if (t == null || t.gameObject == null) continue;
                if (IsPlayerObject(t.gameObject)) { foundPlayer = t.gameObject; break; }
            }
            if (foundPlayer != null) break;
        }

        if (foundPlayer != null)
        {
            if (!foundPlayer.activeInHierarchy)
            {
                Debug.LogError("PlayerGuardian: Player found but inactive — re-enabling.");
                foundPlayer.SetActive(true);
            }
        }
        else
        {
            // Player object not present in scene; don't auto-spawn to avoid creating duplicates on scene loads.
            // Log once so developer can investigate.
            Debug.LogWarning("PlayerGuardian: No Player object found in active scene.");
        }
    }

    private bool IsPlayerObject(GameObject go)
    {
        if (go == null) return false;
        if (go.CompareTag("Player")) return true;
        if (go.name.ToLower().Contains("player")) return true;
        return false;
    }
}
