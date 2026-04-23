using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currentState { get; private set; }
    private UnityEvent<GameState> OnStateChanged;


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateState(GameState.MainMenu);
    }

    public void UpdateState(GameState newState)
    {
       currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                Menu();
                break;
             case GameState.Playing:
                Play();
                break;
            case GameState.Paused:
                PauseGame();
                break;
            case GameState.GameOver:
                GameOver();
                break;

        }
        OnStateChanged?.Invoke(newState);
    }
    void Update()
    {
        if (currentState == GameState.Paused && Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        } 
        else if (currentState != GameState.Paused && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        else if (currentState == GameState.GameOver && Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
        }
    }

    private void Menu()
    {
        Time.timeScale = 1f;
        Debug.Log("Ada di menu");
    }
    private void Play()
    {
        Time.timeScale = 1f;
        Debug.Log("memulai gem");
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("gem berhenti");
        currentState = GameState.Paused;
    }
     public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Game Diulang");
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        Debug.Log("Gem di lanjutkan");
        currentState = GameState.Playing;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over");
        currentState = GameState.GameOver;
    }
       
}