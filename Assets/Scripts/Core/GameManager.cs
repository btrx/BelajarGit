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
        if (currentState == GameState.MainMenu)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Playing();
                UIManager.Instance.StartPlaying();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && currentState == GameState.Playing)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        currentState = GameState.GameOver;
    }

    public void MainMenu()
    {
        Time.timeScale = 0f;
        currentState = GameState.MainMenu;
    }

    public void Playing()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }
}