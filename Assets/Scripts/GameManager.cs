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

    if (currentState == GameState.GameOver &&
        Input.GetKeyDown(KeyCode.R))
    {
        RestartGame();
    }
}

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;

        Debug.Log("Game Paused");
        // Pause system updated
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;

        Debug.Log("Game Resume");
    }

    public void GameOver()
    {
        Debug.Log("Game Over");

        Time.timeScale = 0f;
        currentState = GameState.GameOver;
    }

//Restart For Uas
   public void RestartGame()
{
    Time.timeScale = 1f;
    Debug.Log("Restart");

    SceneManager.LoadScene("Game");
}
    }