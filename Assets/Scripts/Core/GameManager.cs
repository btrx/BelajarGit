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
        currentState = GameState.MainMenu;
        Time.timeScale = 0f;
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
                StartGame();
            }
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        Debug.Log("Game Playing");
    } 

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;
        Debug.Log("Game Paused");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        currentState = GameState.GameOver;
        Debug.Log("Game Over");
    }


}