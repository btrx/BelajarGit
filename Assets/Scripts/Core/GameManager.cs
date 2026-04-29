using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // ✅ tambahan

public class GameManager : MonoBehaviour
{
    public GameObject PausePanel;
    public static GameManager Instance;

    public GameState currentState;
    public GameObject gameOverPanel;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetState(GameState.Playing);

        // pastikan pause panel mati saat awal
        if (PausePanel != null)
            PausePanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("ESC kepencet");

            if (currentState == GameState.Playing)
            {
                SetState(GameState.Pause);
            }
            else if (currentState == GameState.Pause)
            {
                SetState(GameState.Playing);
            }
        }
    }

    public void SetState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.MainMenu:
                Time.timeScale = 0f;
                break;

            case GameState.Playing:
                Time.timeScale = 1f;

                if (PausePanel != null)
                    PausePanel.SetActive(false);
                break;

            case GameState.Pause:
                Time.timeScale = 0f;

                if (PausePanel != null)
                    PausePanel.SetActive(true);
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;

                if (gameOverPanel != null)
                    gameOverPanel.SetActive(true);
                break;
        }
    }

    // ✅ TAMBAHAN TOMBOL
    public void OnResumeButton()
    {
        SetState(GameState.Playing);
    }

    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}