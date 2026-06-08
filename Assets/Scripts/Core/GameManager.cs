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
        // Cek kita ada di scene mana
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "MainMenu")
            ChangeState(GameState.MainMenu);
        else
            ChangeState(GameState.Playing);
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
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log("State changed to: " + newState);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        ChangeState(GameState.Paused);
        UIManager.Instance.ShowPauseMenu(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        ChangeState(GameState.Playing);
        UIManager.Instance.ShowPauseMenu(false);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
        ChangeState(GameState.GameOver);
        UIManager.Instance.ShowGameOverMenu(true);
    }
}