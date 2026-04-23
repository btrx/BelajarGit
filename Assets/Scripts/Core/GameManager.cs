
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            currentState = GameState.MainMenu;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //pindah ke Awake()
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
        
        if(currentState == GameState.GameOver)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
            if(Input.GetKeyDown(KeyCode.M))
            {
                Menu();
            }
        }
        if(currentState == GameState.MainMenu)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }
    }
    public void StartGame()
    {

        Time.timeScale = 1f;
        currentState = GameState.Playing;
        SceneManager.LoadScene("Game");
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
    public void RestartGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        currentState = GameState.MainMenu;
        SceneManager.LoadScene("MainMenu");
    }

}