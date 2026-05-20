using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currentState { get; private set; }
    

    private UnityEvent<GameState> OnStateChanged ;
    private GameObject panelPaused; 
    private GameObject panelGameOver;
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
        currentState = GameState.MainMenu;
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
                HandleGameOver();
                break;
        }

        OnStateChanged?.Invoke(newState);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing) UpdateState(GameState.Paused);
            else if (currentState == GameState.Paused) UpdateState(GameState.Playing);
        }

        if (currentState == GameState.GameOver && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    private void HandleMainMenu(){
        Time.timeScale = 0f;
        Debug.Log("Main Menu");
    }

    private void HandlePlaying(){
        Time.timeScale = 1f;
        Debug.Log("Playing Game");
    }

    private void HandleGameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over Tekan space");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Paused Game");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Resume(){
        UpdateState(GameState.Playing);
    }

    public void GameOver()
    {
        UpdateState(GameState.GameOver);
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