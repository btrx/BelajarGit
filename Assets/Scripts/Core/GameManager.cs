using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentState = GameState.MainMenu;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
        {
            PauseGame();
        }
        else if (currentState == GameState.Paused)
        {
            StartGame();
        }
        }
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");
        Time.timeScale = 0f;
        currentState = GameState.Paused;
    }

    public void StartGame()
    {
        Debug.Log("Game Started");
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        currentState = GameState.GameOver;
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Debug.Log("Restart game");
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        Debug.Log("Back to menu");
        currentState = GameState.MainMenu;
        Time.timeScale = 0f;
    }
}