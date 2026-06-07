using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameState currentState;

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
        ChangeState(GameState.MainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
                ChangeState(GameState.Paused);
            else if (currentState == GameState.Paused)
                ChangeState(GameState.Playing);
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1f;
                Debug.Log("Main Menu");
                break;
            case GameState.Playing:
                Time.timeScale = 1f;
                Debug.Log("Playing");
                Object.FindFirstObjectByType<UIManager>().ShowResumed();
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                Debug.Log("Paused");
                Object.FindFirstObjectByType<UIManager>().ShowPaused();
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                Debug.Log("Game Over");
                Object.FindFirstObjectByType<UIManager>().ShowGameOver();
                break;
        }
    }

    public void StartGame() => ChangeState(GameState.Playing);
    public void PauseGame() => ChangeState(GameState.Paused);
    public void ResumeGame() => ChangeState(GameState.Playing);
    public void GameOver() => ChangeState(GameState.GameOver);
}