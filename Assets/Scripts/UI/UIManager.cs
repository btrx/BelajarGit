using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void Pause()
    {
        GameManager.Instance.PauseGame();
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        GameManager.Instance.ResumeGame();
        Time.timeScale = 1f;
    }
    public void GameOver()
    {
        GameManager.Instance.GameOver();
    }
    public void gameRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BacktoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}