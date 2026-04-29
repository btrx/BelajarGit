using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject mainMenuUI;
    public GameObject pauseMenuUI;

    void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        mainMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void PausedGame()
    {
        pauseMenuUI.SetActive(true);
    }

    public void StartPlaying()
    {
        mainMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}