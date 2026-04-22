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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetState (GameState.MainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
                SetState (GameState.Paused);
            else if (currentState == GameState.Paused)
                SetState (GameState.Playing);
        }
    }

    public void SetState(GameState newState)
    {
        currentState = newState;
        Debug.Log("State: " + currentState);

        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 0f;
                break;
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                break;
        }
    }

   public void StartGame()
    {
        Time.timeScale = 1f;
        SetState (GameState.Playing);
    }

  