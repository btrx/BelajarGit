using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState { get; private set; }
    private UnityEngine.Events.UnityEvent<GameState> OnStateChanged = new UnityEngine.Events.UnityEvent<GameState>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

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
        MainMenu();
        break;

    case GameState.Playing:
        Playing();
        break;

    case GameState.Paused:
        PauseState();
        break;

    case GameState.GameOver:
        GameOverState();
        break;
}

        OnStateChanged?.Invoke(newState);
    }


    void Update()
{
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        if (currentState == GameState.Playing)
            PauseGame();
        else if (currentState == GameState.Paused)
            ResumeGame();
    }

    if (currentState == GameState.GameOver)
    {
        if (Input.GetKeyDown(KeyCode.R))
            StartGame();

        if (Input.GetKeyDown(KeyCode.M))
            UpdateState(GameState.MainMenu);
    }
}

    private void MainMenu()
    {
        Time.timeScale = 0f;
        Debug.Log("Main Menu");
        currentState = GameState.MainMenu;
    }

    private void Playing()
    {
        Time.timeScale = 1f; 
        Debug.Log("Game playing");
        currentState = GameState.Playing;
    }

    private void PauseState()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Paused");
        currentState = GameState.Paused;
    }

    private void GameOverState(){
    Time.timeScale = 0f;
    Debug.Log("Game Over!");
    currentState = GameState.GameOver;
    }

    public void GameOver(){
    UpdateState(GameState.GameOver);}
//test
    public void StartGame() => UpdateState(GameState.Playing);
    public void PauseGame() => UpdateState(GameState.Paused);
    public void ResumeGame() => UpdateState(GameState.Playing);
    public void QuitGame() 
    {
        Debug.Log("Quitting App...");
        Application.Quit(); 
    }
}