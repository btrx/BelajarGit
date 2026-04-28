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
            PlayingGame();
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
            PlayingGame();
        }
       }
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");
        Time.timeScale = 0f;
        currentState = GameState.Paused;
    }

       public void PlayingGame()
    {
        Debug.Log("Game Playing");
        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 1f;
        currentState = GameState.GameOver;
        SceneManager.LoadScene("MainMenu");
    }
    
}