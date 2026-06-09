using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public UnityEngine.UI.Button resumeButton;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindFirstObjectByType<GameManager>();
            return instance;
        }
    }

    public GameState currentState;
    public GameState CurrentState => currentState;

    void OnEnable()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        SyncStateWithActiveScene(SceneManager.GetActiveScene().name);
        GameObject resumeButtonObj = GameObject.FindWithTag("Resume");
        if (resumeButtonObj != null)
        {
            resumeButton = resumeButtonObj.GetComponent<UnityEngine.UI.Button>();
            resumeButton.gameObject.SetActive(false);
        }
        SyncStateWithActiveScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (currentState == GameState.Playing && Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }

    public void StartGame()
    {
        SetState(GameState.Playing);
    }

    public void PauseGame()
    {
        if (currentState != GameState.Playing) return;
        Time.timeScale = 0f;
        currentState = GameState.Paused;
        if (resumeButton != null)
            resumeButton.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        if (currentState != GameState.Paused) return;
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        if (resumeButton != null)
            resumeButton.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 1f;
        currentState = GameState.GameOver;
        SceneManager.LoadScene("GameOver");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SetState(GameState.MainMenu);
        SceneManager.LoadScene("MainMenu");
    }

    void SetState(GameState newState)
    {
        currentState = newState;
        Time.timeScale = newState == GameState.Paused || newState == GameState.GameOver ? 0f : 1f;
    }

    void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SyncStateWithActiveScene(scene.name);
    }

    void SyncStateWithActiveScene(string sceneName)
    {
        if (sceneName == "Game")
        {
            SetState(GameState.Playing);
            return;
        }
        if (sceneName == "GameOver")
        {
            SetState(GameState.GameOver);
            return;
        }
        SetState(GameState.MainMenu);
    }
}