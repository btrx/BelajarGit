using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject gameOverPanel;

    public static GameManager Instance;
    public GameState currentState;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetState(GameState.Playing);

        // matikan panel di awal
        if (PausePanel != null)
            PausePanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        // ESC = Pause / Resume
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (currentState == GameState.Playing)
            {
                SetState(GameState.Pause);
            }
            else if (currentState == GameState.Pause)
            {
                SetState(GameState.Playing);
            }
        }

        // G = TEST Game Over
        if (Keyboard.current != null && Keyboard.current.gKey.wasPressedThisFrame)
        {
            SetState(GameState.GameOver);
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

                if (gameOverPanel != null)
                    gameOverPanel.SetActive(false);
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

    // ================= BUTTON =================

    public void OnResumeButton()
    {
        SetState(GameState.Playing);
    }

    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}