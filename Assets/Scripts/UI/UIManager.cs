using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    void Update()
    {
        if (GameManager.Instance == null) return;

        if (pausePanel != null)
            pausePanel.SetActive(GameManager.Instance.currentState == GameState.Paused);
            
        if (gameOverPanel != null)
            gameOverPanel.SetActive(GameManager.Instance.currentState == GameState.GameOver);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Resume()
    {
        GameManager.Instance.ResumeGame();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}