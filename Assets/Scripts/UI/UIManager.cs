using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

public void Start()
{
    UpdatePanels(GameManager.Instance.currentState);
}
    public void StartGame()
    {
        GameManager.Instance.StartGame();
        SceneManager.LoadScene("Game");
    }

    public void PauseGame()
    {
        GameManager.Instance.PauseGame();
        UpdatePanels(GameState.Pause);
    }

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
        UpdatePanels(GameState.Playing);
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void MenuGame()
    {
        GameManager.Instance.MenuGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void UpdatePanels(GameState state)
    {
        if (mainMenuPanel != null )
        mainMenuPanel.SetActive(state == GameState.MainMenu);

         if (pausePanel != null )
        pausePanel.SetActive(state==GameState.Pause);

        
         if (gameOverPanel != null )
        gameOverPanel.SetActive(state==GameState.GameOver);
    }
}