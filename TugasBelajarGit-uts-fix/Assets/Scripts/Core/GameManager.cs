using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState { get; private set; }
    public UnityEvent<GameState> onGameStateChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Start()
    {
        // currentState = GameState.Playing;
        UpdateStatus(GameState.MainMenu);
    }

   void UpdateStatus(GameState newState)
    {      
        currentState = newState;
       
        switch (newState)
        {
            case GameState.MainMenu:
                HandleMainMenu();
                Debug.Log("Main Menu");
                break;

            case GameState.Playing:
                HandlePlaying();
                Debug.Log("Playing");
                break;

            case GameState.Paused:
                HandlePaused();
                Debug.Log("Paused");
                break;

            case GameState.GameOver:
                HandleGameOver();
                Debug.Log("Game Over");
                break;
        }

        onGameStateChanged?.Invoke(newState);
    }

    private void HandleMainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Back to Main Menu");
    }
    public void HandleGameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over");
    }

    private void HandlePlaying()
    {
        Time.timeScale = 1f; // Ensure game logic is running
        Debug.Log("Game Started/Resumed");
    }

    private void HandlePaused()
    {
        Time.timeScale = 0f; // Freezes physics and frame-rate dependent logic
        Debug.Log("Game Paused");
    }

    public void StartGame() => UpdateStatus(GameState.Playing);
    public void PauseGame() => UpdateStatus(GameState.Paused);
    public void ResumeGame() => UpdateStatus(GameState.Playing);

    public void GameOver()
    {
        UpdateStatus(GameState.GameOver);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing) UpdateStatus(GameState.Paused);
            else if (currentState == GameState.Paused) UpdateStatus(GameState.Playing);
        }

    }
}