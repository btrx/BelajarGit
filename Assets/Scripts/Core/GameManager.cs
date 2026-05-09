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
        ChangeState(GameState.Playing);
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
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        ChangeState(GameState.Paused);
        UIManager.Instance.ShowPausePanel(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        ChangeState(GameState.Playing);
        UIManager.Instance.ShowPausePanel(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        ChangeState(GameState.GameOver);
        UIManager.Instance.ShowGameOverPanel(true);
    }
}