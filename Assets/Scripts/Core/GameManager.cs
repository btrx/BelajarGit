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
    if (currentState == GameState.GameOver && Input.GetKeyDown(KeyCode.R))
    {
        RestartGame();
    }

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
    }
}

    public void StartGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        currentState = GameState.Paused;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
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
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}