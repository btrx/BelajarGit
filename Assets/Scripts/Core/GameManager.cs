using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    public UIManager uiManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        currentState = GameState.Playing;

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (uiManager == null) return;

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

        if (uiManager != null)
        {
            uiManager.ShowGameOver();
        }

        Debug.Log("Game Over");
    }
}