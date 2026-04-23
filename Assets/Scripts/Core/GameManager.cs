
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentState = GameState.Playing;
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentState == GameState.Playing)
                PauseGame();
            else if(currentState == GameState.Paused)
                ResumeGame();
        }
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        UnityEngine.Debug.Log("Game Over");
        currentState = GameState.GameOver;
    }

}