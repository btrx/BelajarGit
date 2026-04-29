using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject pauseUI;
    public GameObject gameOverUI;

    private bool isPaused = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

     
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameOver();
        }
    }

   
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            if (pauseUI != null)
                pauseUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            if (pauseUI != null)
                pauseUI.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseUI != null)
            pauseUI.SetActive(false);
    }


    public void GameOver()
    {
        Debug.Log("GAME OVER");

        Time.timeScale = 0f;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            gameOverUI.transform.SetAsLastSibling();
        }
        else
        {
            Debug.LogError("gameOverUI belum di-assign di Inspector!");
        }
    }

 
    public void BackToMainMenu()
    {
        Debug.Log("Back to Main Menu Klik");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
    }
}