using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

   void Awake()
{
    if (GameManager.Instance != null)
    {
        GameManager.Instance.OnStateChanged.AddListener(OnStateChanged);
        OnStateChanged(GameManager.Instance.currentState);
    }
}

    void OnStateChanged (GameState newState)
    {
        if (mainMenuPanel!= null)mainMenuPanel.SetActive(false);
        if (pausePanel!= null)pausePanel.SetActive(false);
        if (gameOverPanel!= null)gameOverPanel.SetActive(false);

        switch (newState)
        {
            case GameState.MainMenu:
                if (mainMenuPanel!= null)mainMenuPanel.SetActive(true);
                break;
            case GameState.Paused:
                 if (pausePanel!= null)pausePanel.SetActive(true);
                break;
            case GameState.GameOver:
                if (gameOverPanel!= null)gameOverPanel.SetActive(true);
                break;
        }
    }
    void OnDestroy()
    {
        if(GameManager.Instance != null)
            GameManager.Instance.OnStateChanged.RemoveListener(OnStateChanged);
    }
    public void StartGame()
    {
        GameManager.Instance.StartGame();
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void PauseGame() => GameManager.Instance.PauseGame();
    public void ResumeGame() => GameManager.Instance.ResumeGame();
    public void PergiKeMainMenu() => GameManager.Instance.PergiKeMainMenu();

}