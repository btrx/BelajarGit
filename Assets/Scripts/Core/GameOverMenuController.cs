using UnityEngine;
using UnityEngine.SceneManagement; // Wajib untuk mengatur scene

public class GameOverMenuController : MonoBehaviour
{
    public void ClickRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game"); 
    }

    public void ClickMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}