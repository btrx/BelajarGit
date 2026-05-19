using System.Data;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;
    public UnityEvent<GameState> OnStateChanged;

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
        UpdateState(GameState.Playing);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC KEPencet" + currentState);
            if (currentState == GameState.Playing)
                UpdateState (GameState.Paused);
            else if (currentState == GameState.Paused)
                UpdateState (GameState.Playing);
        }
    }

    public void UpdateState (GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu: Time.timeScale = 1f; break;
            case GameState.Playing: Time.timeScale = 1f; break;
            case GameState.Paused: Time.timeScale = 0f; break;
            case GameState.GameOver: Time.timeScale = 0f; break;
        }

        OnStateChanged?.Invoke(newState);
    }

    public void PauseGame()
    {
        UpdateState(GameState.Paused);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        UpdateState (GameState.GameOver);
    }

    public void StartGame() => UpdateState(GameState.Playing);
    public void ResumeGame() => UpdateState (GameState.Playing);

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PergiKeMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() => Application.Quit();
}