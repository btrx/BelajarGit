using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Dipanggil tombol Play di MainMenu
    public void StartGame()
    {
        // Set state ke Playing dulu
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetState(GameManager.GameState.Playing);
            GameManager.Instance.StartGame(); // ini akan load scene Gameplay
        }
        else
        {
            SceneManager.LoadScene("Game"); // fallback jika GameManager tidak ada
        }
    }

    // Dipanggil tombol Restart saat GameOver
    public void RestartGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetState(GameManager.GameState.Playing);
            GameManager.Instance.StartGame();
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }

    // Dipanggil tombol Resume saat Pause
    public void ResumeGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResumeGame();
        }
    }

    // Dipanggil tombol Pause (bisa dari UI)
    public void PauseGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PauseGame();
        }
    }

    // Dipanggil tombol Back to Main Menu
    public void BackToMainMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.BackToMainMenu();
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}