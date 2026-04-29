using UnityEngine;
using UnityEngine.SceneManagement;

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
        Time.timeScale = 1f;
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
        Debug.Log("Game Paused");
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        Debug.Log("Game Resumed");
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        currentState = GameState.GameOver;
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