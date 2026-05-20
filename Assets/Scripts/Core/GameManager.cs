using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

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
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    void Update()
    {
        // hanya bisa pause/resume saat sedang bermain, dan hanya bisa restart/back to menu saat game over
        if (currentState == GameState.Playing && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        else if (currentState == GameState.Paused && Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
        else if (currentState == GameState.GameOver && Input.GetKeyDown(KeyCode.Q))
        {
            RestartGame();
        }
        else if (currentState == GameState.GameOver && Input.GetKeyDown(KeyCode.M))
        {
            BackToMenu();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        currentState = GameState.GameOver;
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ClearConsole();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        ClearConsole();
    }

    void ClearConsole()
    {
    #if UNITY_EDITOR
        var LogEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        var ClearMethod = LogEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        ClearMethod.Invoke(null, null);
    #endif
    }
}