using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        Playing,
        Pause,
        GameOver
    }

    public static GameManager Instance;
    public GameState currentState;

    [Header("UI References")]
    public GameObject pausePanel; // ← Drag PausePanel ke sini nanti

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
        SetState(GameState.MainMenu);
    }

    public void SetState(GameState newState)
    {
        currentState = newState;
        Debug.Log("Game State: " + currentState);

        switch (currentState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                if (pausePanel != null) pausePanel.SetActive(false);
                break;
            case GameState.Pause:
                Time.timeScale = 0f;
                if (pausePanel != null) pausePanel.SetActive(true);
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                break;
            case GameState.MainMenu:
                Time.timeScale = 1f;
                break;
        }
    }

    public void StartGame()
    {
        SetState(GameState.Playing);
        SceneManager.LoadScene("Game");
    }

    public void PauseGame()
    {
        if (currentState == GameState.Playing)
        {
            SetState(GameState.Pause);
        }
    }

    public void ResumeGame()
    {
        Debug.Log("ResumeGame dipanggil!"); // ← cek apakah tombol bekerja
        if (currentState == GameState.Pause)
        {
            SetState(GameState.Playing);
        }
    }

    public void GameOver()
    {
        SetState(GameState.GameOver);
    }

    public void BackToMainMenu()
    {
        SetState(GameState.MainMenu);
        SceneManager.LoadScene("MainMenu");
    }
}