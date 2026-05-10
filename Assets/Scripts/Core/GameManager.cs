using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    public UIManager uiManager;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                uiManager.PauseGame();
            }
            else if (currentState == GameState.Paused)
            {
                uiManager.ResumeGame();
            }
        }
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;

        Time.timeScale = 0f;

        uiManager.ShowGameOver();

        Debug.Log("Game Over");
    }
}