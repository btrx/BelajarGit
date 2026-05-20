using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState { get; private set; }
    public UnityEvent<GameState> OnStateChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        UpdateState(GameState.MainMenu);
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }

        if (currentState == GameState.GameOver && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    public void UpdateState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1f;
                Debug.Log("Kembali ke menu");
                break;

            case GameState.Playing:
                Time.timeScale = 1f;
                Debug.Log("Game Started");
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                Debug.Log("Game Paused");
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                Debug.Log("Game Over");
                break;
        }

        OnStateChanged?.Invoke(currentState);
    }

    public void PauseGame()
    {
        UpdateState(GameState.Paused);
    }

    public void Resume()
    {
        UpdateState(GameState.Playing);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Game Restarted");
        StartGame();
    }

    public void GameOver()
    {
        UpdateState(GameState.GameOver);
    }

    public void StartGame()
    {
        UpdateState(GameState.Playing);
    }

    public void OnPause()
    {
        UpdateState(GameState.Paused);
    }

    public void ResumeGame()
    {
        UpdateState(GameState.Playing);
    }

    public void Over()
    {
        UpdateState(GameState.GameOver);
    }
}