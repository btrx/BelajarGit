using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GameState _currentState;
    public GameState CurrentState => _currentState;

    void Awake()
    {
        // Singleton guard: destroy duplicates
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: persist across scenes
    }

    void Start()
    {
        SetState(GameState.MainMenu);
    }

    // Centralized state transitions — side-effects fire ONCE on change
    public void SetState(GameState newState)
    {
        if (_currentState == newState) return;

        _currentState = newState;

        switch (_currentState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1f;
                Debug.Log("Entered: Main Menu");
                break;

            case GameState.Playing:
                Time.timeScale = 1f;
                Debug.Log("Entered: Playing");
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                Debug.Log("Entered: Paused");
                break;

            case GameState.GameOver:
                Time.timeScale = 0f; // Optionally freeze on game over
                Debug.Log("Entered: Game Over");
                break;

            default:
                Debug.LogWarning($"Unhandled state: {_currentState}");
                break;
        }
    }

    public void StartGame()  => SetState(GameState.Playing);
    public void PauseGame()  => SetState(GameState.Paused);
    public void ResumeGame() => SetState(GameState.Playing);
    public void GameOver()   => SetState(GameState.GameOver);

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}