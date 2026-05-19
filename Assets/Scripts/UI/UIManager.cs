using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{   
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }
}