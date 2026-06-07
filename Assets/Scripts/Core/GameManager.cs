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
        currentState = GameState.Playing;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
    {
        PauseGame();
    }

    if (currentState == GameState.MainMenu)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            currentState = GameState.Playing;
        }
    }

    if (Input.GetKeyDown(KeyCode.R))
    {
        RestartGame();
    }
    }

  public void PauseGame()
    {
        if (currentState == GameState.Playing)
        {
            Time.timeScale = 0f;
            currentState = GameState.Paused;
        }
        else if (currentState == GameState.Paused)
        {
            Time.timeScale = 1f;
            currentState = GameState.Playing;
        }
    }    

  public void ResumeGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }    

    public void GameOver()
    {
        Debug.Log("Game Over");

        Time.timeScale = 0f;
        currentState = GameState.GameOver;
    }

     public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
        SceneManager.GetActiveScene().buildIndex
        );
    }   
}