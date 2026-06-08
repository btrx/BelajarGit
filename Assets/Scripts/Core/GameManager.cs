using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        if(pausePanel != null) pausePanel.SetActive(false);
        if(gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing) PauseGame();
            else if (currentState == GameState.Paused) ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        if(pausePanel != null) pausePanel.SetActive(true);
        currentState = GameState.Paused;
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if(pausePanel != null) pausePanel.SetActive(false);
        currentState = GameState.Playing;
        Debug.Log("Game Resumed");
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
        if(gameOverPanel != null) gameOverPanel.SetActive(true);
        currentState = GameState.GameOver;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game Restarted");
    }

    public void LoadMainMenu()
    {
        Debug.Log("gas pencet play");
        SceneManager.LoadScene("MainMenu");
    }
}