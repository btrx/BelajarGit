using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject gameOverUI;

    void Start()
    {
        ShowMainMenu();
    }

    public void StartGame()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.StartGame();
    }

    public void ShowMainMenu()
    {
        mainMenuUI.SetActive(true);
        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
    }

    public void ShowPause()
    {
        mainMenuUI.SetActive(false);
        pauseUI.SetActive(true);
        gameOverUI.SetActive(false);
    }

    public void Resume()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ResumeGame();

        ShowMainMenu();
    }

    public void ShowGameOver()
    {
        mainMenuUI.SetActive(false);
        pauseUI.SetActive(false);
        gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.StartGame();
    }

    public void Menu()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.BackToMenu();
    }
}