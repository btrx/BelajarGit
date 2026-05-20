using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currentState;
    public GameObject restartButton;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject mainMenu;
    public float speed = 3f;

    // HEALTH
    public int maxHealth = 100;
    public int currentHealth;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        currentHealth = maxHealth;
        ResumeGame();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                PauseGame();
            }
            else if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;
        pauseMenu.SetActive(true);
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        pauseMenu.SetActive(false);
        Debug.Log("Game Resumed");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        Debug.Log("Returned to Main Menu");
    }

    // KENA DAMAGE
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Health : " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameOver();
        }
    }

    // TAMBAH HEALTH
    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log("Health : " + currentHealth);
    }

    
     public void GameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over");
        currentState = GameState.GameOver;
        gameOverMenu.SetActive(true);
        pauseMenu.SetActive(false); 
        }
}