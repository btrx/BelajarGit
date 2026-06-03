using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    [Header("UI")]
    public GameObject pauseUI;
    public GameObject gameOverUI;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;

        if (pauseUI != null)
            pauseUI.SetActive(false);

        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GameOver();
        }
    }

    
    public void TogglePause()
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

    public void PauseGame()
    {
        currentState = GameState.Paused;
        Time.timeScale = 0f;

        if (pauseUI != null)
            pauseUI.SetActive(true);
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;

        if (pauseUI != null)
            pauseUI.SetActive(false);
    }

    
    public void GameOver()
    {
        Debug.Log("GAME OVER");

        currentState = GameState.GameOver;
        Time.timeScale = 0f;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            gameOverUI.transform.SetAsLastSibling();
        }
    }

   
    public void BackToMainMenu()
    {
        Debug.Log("Back to Main Menu");

        Time.timeScale = 1f;
        currentState = GameState.Playing;

        SceneManager.LoadScene("MainMenu");
    }
}