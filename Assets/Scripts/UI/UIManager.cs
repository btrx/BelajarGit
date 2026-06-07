using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausedPanel;

    void Start()
    {
        if (pausedPanel != null)
            pausedPanel.SetActive(false);
            
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
        SceneManager.LoadScene("Game");
    }

    public void PauseGame()
    {
        GameManager.Instance.PauseGame();
    }

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameManager.Instance.StartGame();
        SceneManager.LoadScene("Game");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ChangeState(GameState.MainMenu);
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void ShowPaused()
    {
        if (pausedPanel != null)
            pausedPanel.SetActive(true);
    }

    public void ShowResumed()
    {
        if (pausedPanel != null)
            pausedPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}