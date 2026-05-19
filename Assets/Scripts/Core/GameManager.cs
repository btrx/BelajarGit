using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    public GameObject gameOverPanel;

    public GameObject pausePanel;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
        {
            PauseGame();
        }
        else if (currentState == GameState.Paused)
        {
            ResumeGame();
        }
        }
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");
        Time.timeScale = 0f;
        currentState = GameState.Paused;

        if (pausePanel != null) pausePanel.SetActive(true);
    }

    public void StartGame()
    {
        Debug.Log("Game Started");
        currentState = GameState.Playing;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void ResumeGame()
    {
        Debug.Log("Resume Game");
        currentState = GameState.Playing;
        Time.timeScale = 1f;

        if (pausePanel != null) pausePanel.SetActive(false);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        currentState = GameState.GameOver;
        Time.timeScale = 0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Debug.Log("Restart game");
        currentState = GameState.Playing;
        Time.timeScale = 1f;

        SceneManager.LoadScene("Game");
    }

    public void BackToMenu()
    {
        Debug.Log("Back to menu");
        currentState = GameState.MainMenu;
        Time.timeScale = 0f;

        SceneManager.LoadScene("MainMenu");
    }
}