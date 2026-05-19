using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    public static GameManager Instance;

    public GameState currentState;

    void Awake()
{
    Instance = this;
}

   public void Startgame()
{
    Time.timeScale = 1f;
    currentState = GameState.Playing;

    if (pauseMenu != null)
        pauseMenu.SetActive(false);

    if (gameOverMenu != null)
        gameOverMenu.SetActive(false);

    SceneManager.LoadScene("Game");
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

    public void PauseGame()
    {
        Time.timeScale = 0f;

        currentState = GameState.Paused;

        if (pauseMenu != null)
            pauseMenu.SetActive(true);

        Debug.Log("Game Paused");
    }

    
    public void ResumeGame()
    {
        Time.timeScale = 1f;

        currentState = GameState.Playing;

        if (pauseMenu != null)
            pauseMenu.SetActive(false);

        Debug.Log("Game Resumed");
    }

   
    public void GameOver()
    {
        Time.timeScale = 0f;

        currentState = GameState.GameOver;

        if (gameOverMenu != null)
            gameOverMenu.SetActive(true);

        Debug.Log("Game Over!");
    }

  
    public void RestartGame()
    {
        Time.timeScale = 1f;

        currentState = GameState.Playing;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

  
   public void RestartToMenu()
{
    Time.timeScale = 1f;
    currentState = GameState.MainMenu;

    SceneManager.LoadScene("MainMenu");
}

  
    public void QuitGame()
    {
        Application.Quit();
    }
}