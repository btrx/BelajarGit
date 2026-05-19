using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    void Start()
    {
        // Pastikan game berjalan normal saat scene dimulai
        Time.timeScale = 1f;

        // Matikan panel saat awal
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    // Start Game
    public void StartGame()
    {
        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.Startgame();
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }

    // Pause Game
    public void PauseGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);

        Time.timeScale = 0f;
    }

    // Resume Game
    public void ResumeGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResumeGame();
        }
    }

    // Game Over
    public void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;

        Debug.Log("Game Over");
    }

    // Restart Current Scene
    public void Restart()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Main Menu
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    // Quit Game
    public void QuitGame()
    {
        Debug.Log("Keluar Game...");
        Application.Quit();
    }
}