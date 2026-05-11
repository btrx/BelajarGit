using UnityEngine;
using System;
 
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
 
    [Header("Game State")]
    public GameState currentState;
 
    [Header("UI Panel")]
    public GameObject mainMenuPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject gameplayPanel;
 
    // Event agar script lain bisa subscribe perubahan state
    public static event Action<GameState> OnStateChanged;
 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
 
    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }
 
    public void ChangeState(GameState newState)
    {
        currentState = newState;
 
        // Sembunyikan semua panel dulu
        mainMenuPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameplayPanel.SetActive(false);
 
        switch (currentState)
        {
            case GameState.MainMenu:
                MainMenuState();
                break;
            case GameState.Playing:
                PlayingState();
                break;
            case GameState.Pause:
                PauseState();
                break;
            case GameState.GameOver:
                GameOverState();
                break;
        }
 
        // Broadcast event ke subscriber (UIManager, dll)
        OnStateChanged?.Invoke(currentState);
    }
 
    private void MainMenuState()
    {
        mainMenuPanel.SetActive(true);
        Time.timeScale = 1f;
    }
 
    private void PlayingState()
    {
        gameplayPanel.SetActive(true);
        Time.timeScale = 1f;
    }
 
    private void PauseState()
    {
        gameplayPanel.SetActive(true);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
 
    private void GameOverState()
    {
        gameplayPanel.SetActive(true);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}