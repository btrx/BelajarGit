using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    [Header("Player HP")]
    public int maxHP = 3;
    public int currentHP;

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
        currentState = GameState.MainMenu;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (currentState == GameState.Playing)
                PauseGame();
            else if (currentState == GameState.Paused)
                ResumeGame();
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;

        ResetHP(); 

        SceneManager.LoadScene("Game");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;
        Debug.Log("GAME PAUSED");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        Debug.Log("GAME RESUMED");
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
        Time.timeScale = 1f;

        Debug.Log("GAME OVER");

        Invoke("GoToMainMenu", 1.5f);
    }

    void GoToMainMenu()
    {
        currentState = GameState.MainMenu;
        SceneManager.LoadScene("MainMenu");
    }

    void ResetHP()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        if (currentState != GameState.Playing) return;

        currentHP -= damage;

        if (currentHP <= 0)
        {
            GameOver();
        }
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        currentState = GameState.MainMenu;
        SceneManager.LoadScene("MainMenu");
    }
}