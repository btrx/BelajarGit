using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    [Header("UI")]
    public UIManager uiManager;

    void Awake()
    {
        // Singleton protection
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        // Saat awal game masuk menu
        currentState = GameState.MainMenu;

        // Pause game di menu
        Time.timeScale = 0f;

        // Update UI awal
        uiManager.UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Jika sedang bermain -> pause
            if (currentState == GameState.Playing)
            {
            
                PauseGame();
            }

            // Jika sedang pause -> resume
            else if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        currentState = GameState.Playing;

        uiManager.UpdateUI();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;

        currentState = GameState.Paused;

        uiManager.UpdateUI();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        currentState = GameState.Playing;

        uiManager.UpdateUI();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        currentState = GameState.GameOver;

        uiManager.UpdateUI();

        Debug.Log("Game Over");
    }
}