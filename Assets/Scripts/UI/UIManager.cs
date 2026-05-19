using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Canvas References")]
    public GameObject pauseCanvas;
    public GameObject gameOverCanvas;
    void Start()
    {
        if (pauseCanvas     != null) pauseCanvas.SetActive(false);
        if (gameOverCanvas  != null) gameOverCanvas.SetActive(false);
    }
        void Update()
        {
            if (GameManager.Instance.currentState == GameState.Paused)
            {
                if (pauseCanvas != null) pauseCanvas.SetActive(true);
            }
            else
            {
                if (pauseCanvas != null) pauseCanvas.SetActive(false);
            }
    
            if (GameManager.Instance.currentState == GameState.GameOver)
            {
                if (gameOverCanvas != null) gameOverCanvas.SetActive(true);
            }
            else
            {
                if (gameOverCanvas != null) gameOverCanvas.SetActive(false);
            }
        }
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
        GameManager.Instance.PlayingGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        GameManager.Instance.PlayingGame();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}