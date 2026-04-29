using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject mainMenuUI;

    void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        GameManager.Instance.MainMenu();
    }

    public void StartPlaying()
    {
        mainMenuUI.SetActive(false);
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