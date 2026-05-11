using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public GameState currentState;

    [Header("UI Panel")]
    public GameObject mainMenuPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject gameplayPanel;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;

        // Reset semua panel
        mainMenuPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameplayPanel.SetActive(false);

        switch (currentState)
        {
            case GameState.MainMenu:
                MainMenuState();
                break;

            case GameState.Playing:
                PlayingState();
                break;

            case GameState.Pause:
                PauseState();
                break;

            case GameState.GameOver:
                GameOverState();
                break;
        }
    }

    void MainMenuState()
    {
        mainMenuPanel.SetActive(true);

        Time.timeScale = 1;
    }

    void PlayingState()
    {
        gameplayPanel.SetActive(true);

        Time.timeScale = 1;
    }

    void PauseState()
    {
        gameplayPanel.SetActive(true);
        pausePanel.SetActive(true);

        Time.timeScale = 0;
    }

    void GameOverState()
    {
        gameOverPanel.SetActive(true);

        Time.timeScale = 0;
    }
}