using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState { get; private set;}
     public UnityEvent<GameState> OnStateChanged;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private void OnMainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Kembali ke menu");
    }
    private void OnPlaying()
    {
        Time.timeScale = 1f;
        Debug.Log("Game Started");
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        currentState = GameState.GameOver;
    }
       public void StartGame() => UpdateState(GameState.Playing);
    public void OnPause() => UpdateState(GameState.Paused);
    public void ResumeGame() => UpdateState(GameState.Playing);
    public void QuitGame() 
    {
        Debug.Log("Keluar dari game....");
        Application.Quit(); 
    }

}