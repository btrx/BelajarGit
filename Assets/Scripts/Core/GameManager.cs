using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    public GameObject pausePanel;
    public GameObject gameOverPanel;

    void Awake()
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

    void Start()
    {
        currentState = GameState.Playing;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
 
public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        ChangeState(GameState.MainMenu);
        Time.timeScale = 1f;
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        currentState = GameState.Playing;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }
public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        currentState = GameState.Paused;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        currentState = GameState.GameOver;
        Debug.Log("Game Over");
    }

    internal void SetState(GameState mainMenu)
    {
        throw new NotImplementedException();
    }
}