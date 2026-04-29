using UnityEngine;

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
        MainMenu();
    }

    void Update()
    {
        if (currentState == GameState.MainMenu && Input.GetKeyDown(KeyCode.Space))
        {
            Playing();
            UIManager.Instance.StartPlaying();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                PauseGame();
                UIManager.Instance.PausedGame();
            }
            else if (currentState == GameState.Paused)
            {
                Playing();
                UIManager.Instance.StartPlaying();
            }else if (currentState == GameState.MainMenu)
            {
                UIManager.Instance.QuitGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentState == GameState.GameOver || currentState == GameState.Paused)
            {
            UIManager.Instance.Restart();           
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over");
        currentState = GameState.GameOver;
        UIManager.Instance.GameOver();
    }

    public void MainMenu()
    {
        Time.timeScale = 0f;
        currentState = GameState.MainMenu;
        UIManager.Instance.StartGame();
    }

    public void Playing()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }
}