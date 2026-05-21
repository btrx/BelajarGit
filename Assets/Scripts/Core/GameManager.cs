using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;
    bool isPaused = false;
    public GameObject GameOverPanel;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentState = GameState.Playing;
        GameOverPanel.SetActive(false);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }   
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        isPaused = false;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over");
        currentState = GameState.GameOver;
        GameOverPanel.SetActive(true);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}