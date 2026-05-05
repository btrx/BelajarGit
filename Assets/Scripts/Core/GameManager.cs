using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton biar bisa dipanggil script lain
    public static GameManager Instance;

    public GameObject gameOverPanel;

    void Awake()
    {
        // Inisialisasi Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f; // Stop waktu
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting...");
        Time.timeScale = 1f; // Balikin waktu ke normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}