using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string gameSceneName = "Game";

    // Start Game
    public void StartGame()
    {
        Time.timeScale = 1f;
        LoadScene(gameSceneName);
    }

    // Restart Game
    public void RestartGame()
    {
        Time.timeScale = 1f;
        LoadScene(gameSceneName);
    }

    // Resume dari Pause
    public void ResumeGame()
    {
        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.currentState = GameState.Playing;
        }
    }

    // Quit Game
    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();
    }

    // Helper Load Scene
    private void LoadScene(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene tidak ditemukan: " + sceneName);
        }
    }
}