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

    void Update()
    {
       
        if (currentState == GameState.MainMenu)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
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

        if (currentState == GameState.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 0;
                Debug.Log("Main Menu - Press SPACE to Start");
                break;

            case GameState.Playing:
                Time.timeScale = 1;
                Debug.Log("Playing");
                break;

            case GameState.Paused:
                Time.timeScale = 0;
                Debug.Log("Paused - Press P to Resume");
                break;

            case GameState.GameOver:
                Time.timeScale = 0;
                Debug.Log("Game Over - Press R to Restart");
                break;
        }
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

    public void LoadGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
}