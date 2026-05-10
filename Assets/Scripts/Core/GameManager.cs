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
        // Pause dan Resume
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                ChangeState(GameState.Pause);
            }
            else if (currentState == GameState.Pause)
            {
                ChangeState(GameState.Playing);
            }
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1;
                break;

            case GameState.Playing:
                Time.timeScale = 1;
                Debug.Log("Game Playing");
                break;

            case GameState.Pause:
                Time.timeScale = 0;
                Debug.Log("Game Paused");
                break;

            case GameState.GameOver:
                Time.timeScale = 0;
                Debug.Log("Game Over");
                break;
        }
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}