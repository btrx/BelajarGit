using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    public GameObject pauseCanvas;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PlayingGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
                PauseGame();
            else if (currentState == GameState.Paused)
                PlayingGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;
        Debug.Log("Game State: " + currentState);
        pauseCanvas.SetActive(true);
    }

    public void PlayingGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        Debug.Log("Game State: " + currentState);
        pauseCanvas.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        currentState = GameState.GameOver;
        Debug.Log("Game State: " + currentState);
        SceneManager.LoadScene("GameOver");
    }
}