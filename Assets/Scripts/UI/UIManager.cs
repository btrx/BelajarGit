using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Panels")]
    public GameObject pauseMenuPanel;
    public GameObject gameOverPanel;

    void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowPauseMenu(bool show)
    {
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(show);
    }

    public void ShowGameOverMenu(bool show)
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(show);
    }
}