using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject panelPaused;
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

    public void ResumeGame()
    {
        panelPaused.SetActive(false);
        Debug.Log("Resuming Game");
        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }

    public void PauseGame()
    {
        panelPaused.SetActive(true);
        Debug.Log("Pausing Game");
        Time.timeScale = 0f;
        currentState = GameState.Paused;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameOver");
        currentState = GameState.GameOver;
        Time.timeScale = 1f;    
    }
}