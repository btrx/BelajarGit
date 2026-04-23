using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState {get; private set;}

    private UnityEvent<GameState> OnStateChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentState = GameState.Playing;
    }

    public void UpdateState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.Playing:
                HandlePlaying();
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Resume();
        }
    }

    private void HandleMainMenu(){
        Time.timeScale = 0f;
        Debug.Log("Main Menu");
        currentState = GameState.MainMenu;
    }

    private void HandlePlaying(){
        Time.timeScale = 1f;
        Debug.Log("Playing Game");
        currentState = GameState.Playing;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Paused Game");
        currentState = GameState.Paused;
    }

    public void Resume(){
        Time.timeScale = 1f;
        Debug.Log("Resume Game");
        currentState = GameState.Playing;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over");
        currentState = GameState.GameOver;
    }
    
    public void StartGame() => UpdateState(GameState.Playing);
    public void Paused() => UpdateState(GameState.Paused);
    public void ResumeGame() => UpdateState(GameState.Playing);
    public void QuitGame() 
    {
        Debug.Log("Keluar dari game....");
        Application.Quit(); 
    }
}