using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currentState;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentState = GameState.MainMenu;
    }

    void Update()
    {
        // tombol P untuk pause / resume
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
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        SceneManager.LoadScene("Game");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;

        Debug.Log("=== GAME PAUSED ===");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;

        Debug.Log("=== GAME RESUMED ===");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        currentState = GameState.GameOver;

        Debug.Log("=== GAME OVER ===");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        SceneManager.LoadScene("Game");
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        currentState = GameState.MainMenu;
        SceneManager.LoadScene("MainMenu");
    }
}