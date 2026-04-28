using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    void Awake()
    {
         if (Instance != null && Instance != this)
    {
        Destroy(gameObject);
        return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (Instance == this)
        currentState = GameState.MainMenu;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            PauseGame();
            else if (currentState == GameState.Pause)
            ResumeGame();
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
        currentState = GameState.Pause;

          UIManager ui = FindObjectOfType<UIManager>();
    if (ui != null) ui.UpdatePanels(GameState.Pause);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
        currentState = GameState.GameOver;

         UIManager ui = FindObjectOfType<UIManager>();
    if (ui != null) ui.UpdatePanels(GameState.GameOver);

    }

    public void RestartGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuGame()
    {
        currentState = GameState.MainMenu;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}