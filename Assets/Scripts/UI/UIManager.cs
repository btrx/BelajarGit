using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    // 2 panel
    public GameObject pausePanel;    
    public GameObject gameOverPanel; 

    public void KeMenuUtama()
    {
        Debug.Log("Mencoba pindah ke MainMenu...");
        Time.timeScale = 1f; 
        
        SceneManager.LoadScene("MainMenu"); 
    }


void Awake()
    {
       
        Instance = this;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeState(GameState.Playing);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game ditutup"); 
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Resume()
    {
        // Menyembunyikan panel saat resume
        if (pausePanel != null) pausePanel.SetActive(false);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResumeGame();
        }
    }

    
    public void ShowPausePanel()
    {
        if (pausePanel != null) pausePanel.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }
}