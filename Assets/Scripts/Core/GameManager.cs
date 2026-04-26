using UnityEngine;
using UnityEngine.SceneManagement;

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
        currentState = GameState.Playing;

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (currentState == GameState.Playing && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        else if (currentState == GameState.Paused && Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
        else if (currentState == GameState.GameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
        else if (currentState == GameState.GameOver && Input.GetKeyDown(KeyCode.M))
        {
            BackToMenu();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;

        currentState = GameState.Paused;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;

        currentState = GameState.Playing;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        currentState = GameState.GameOver;

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 

    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}