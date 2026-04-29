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
        Time.timeScale = 1f;
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
                ResumeGame();
            }
            PauseGame();
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;
        Debug.Log("Game paused");
    }

    public void ResumeGame()
    {
        Time.timeScale =1f;
        currentState = GameState.Playing;
        Debug.Log("Game Resumed");

    }


    public void GameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over");
        currentState = GameState.GameOver;
       
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }
}