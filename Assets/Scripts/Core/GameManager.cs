using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                    Debug.Log("GameManager auto-created because no instance was found in scene.");
                }
            }
            return instance;
        }
    }

    public GameState currentState;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager Awake: instance set");
        }
        else if (instance != this)
        {
            Debug.Log("GameManager Awake: duplicate instance destroyed");
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (currentState == GameState.MainMenu || currentState == GameState.GameOver || currentState == GameState.Paused)
        {
            currentState = GameState.Playing;
        }
        else if (currentState == default)
        {
            currentState = GameState.Playing;
        }
        Debug.Log($"GameManager Start: currentState={currentState}");
    }

    void Update()
    {
        bool escapePressed = (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame) || Input.GetKeyDown(KeyCode.Escape);
        bool spacePressed = (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) || Input.GetKeyDown(KeyCode.Space);

        if (escapePressed)
        {
            Debug.Log($"Pause input detected. currentState={currentState}");
            if (currentState == GameState.Playing)
                PauseGame();
            else if (currentState == GameState.Paused)
                ResumeGame();
        }

        if (spacePressed && currentState != GameState.Playing)
        {
            Debug.Log($"Restart input detected. currentState={currentState}");
            RestartGame();
        }
    }
    public void StartGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }
   
    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;
        Debug.Log("GameManager: Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        Debug.Log("GameManager: Resumed");
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        currentState = GameState.GameOver;
    }

    public void RestartGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("GameManager: Restarted");
    }
}