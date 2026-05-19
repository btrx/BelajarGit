using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && currentState == GameState.Playing)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && currentState == GameState.Paused)
        {
            PlayingGame();
        }
    }
    public void PlayingGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;

        Debug.Log("State: Playing");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;

        Debug.Log("State: Paused");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        currentState = GameState.GameOver;

        Debug.Log("State: Game Over");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        currentState = GameState.MainMenu;

        Debug.Log("State: Main Menu");
    }
}