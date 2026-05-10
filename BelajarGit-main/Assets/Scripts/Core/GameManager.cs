using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    ChangeState(GameState.Playing);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 0;
                Debug.Log("Main Menu");
                break;

            case GameState.Playing:
                Time.timeScale = 1;
                Debug.Log("Playing");
                break;

            case GameState.Paused:
                Time.timeScale = 0;
                Debug.Log("Pause");
                break;

            case GameState.GameOver:
                Time.timeScale = 0;
                Debug.Log("Game Over");
                break;
        }
    }

    public void StartGame()
    {
        ChangeState(GameState.Playing);
    }

    public void PauseGame()
    {
        ChangeState(GameState.Paused);
    }

    public void ResumeGame()
    {
        ChangeState(GameState.Playing);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}