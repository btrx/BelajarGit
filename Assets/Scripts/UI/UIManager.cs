using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pausePanel; 
    public GameObject gameOverPanel;

    public void StartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Game");
    }
    
}