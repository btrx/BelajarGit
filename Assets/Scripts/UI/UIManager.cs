using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseCanvas;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

     public void Restart()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BtnPause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Game Paused");
    }

    public void BtnResume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Game Resumed");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}