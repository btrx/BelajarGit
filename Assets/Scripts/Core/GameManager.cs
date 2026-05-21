using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currentState { get; private set; }
    [SerializeField] private UIManager uiManager;
    private UnityEvent<GameState> OnStateChanged;


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateState(GameState.MainMenu);
    }

    public void UpdateState(GameState newState)
    {
       currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                OnMainMenu();
                break;
             case GameState.Playing:
                OnPlaying();
                break;
            case GameState.Paused:
                PauseGame();
                break;
            case GameState.GameOver:
                GameOver();
                break;

        }
        OnStateChanged?.Invoke(newState);
    }
    void Update()
    {
        if (currentState == GameState.Paused && Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        } 
        else if (currentState != GameState.Paused && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        else if (currentState == GameState.GameOver && Input.GetKeyDown(KeyCode.Space))
        {
            uiManager.Restart();
        }
    }

    private void OnMainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Lagi di menu");
    }
    private void OnPlaying()
    {
        Time.timeScale = 1f;
        Debug.Log("Game dimulai");
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Stopped");
        currentState = GameState.Paused;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        Debug.Log("Game jalan lagi");
        currentState = GameState.Playing;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("kalah");
        currentState = GameState.GameOver;
    }
       public void StartGame() => UpdateState(GameState.Playing);
        public void OnPause() => UpdateState(GameState.Paused);
        public void ResumeGame() => UpdateState(GameState.Playing);
        public void Defeat() => UpdateState(GameState.GameOver);
}