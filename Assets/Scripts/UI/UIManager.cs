using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pausePanel; 
    public GameObject gameOverPanel;

    public void StartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Game");
    }
    public void PauseGame()
    {
        GameManager.Instance.currentState = GameState.Paused;

        Time.timeScale = 0f;

        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        GameManager.Instance.currentState = GameState.Playing;

        Time.timeScale = 1f;

        pausePanel.SetActive(false);
    }
     public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
    public void BackToMenu()
    {
    Time.timeScale = 1f;

    SceneManager.LoadScene("MainMenu");
    }
    
}