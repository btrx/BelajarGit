using UnityEngine;
using UnityEngine.SceneManagement;
 
public class UIManager : MonoBehaviour
{
    // UIManager hanya berisi tombol-tombol UI.
    // Panel sudah dikelola oleh GameManager.ChangeState().
 
    // ----- Tombol Main Menu -----
    public void StartGame()
    {
        // Jika game dan menu ada di scene yang sama:
        GameManager.Instance.ChangeState(GameState.Playing);
 
        // Jika game ada di scene terpisah, uncomment baris ini dan comment baris atas:
        // SceneManager.LoadScene("Game");
    }
 
    // ----- Tombol Pause -----
    public void Pause()
    {
        if (GameManager.Instance.currentState == GameState.Playing)
        {
            GameManager.Instance.ChangeState(GameState.Pause);
        }
    }
 
    public void Resume()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
    }
 
    // ----- Tombol Game Over -----
    public void Restart()
    {
        Time.timeScale = 1f; // pastikan timeScale normal sebelum reload
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
 
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        // Jika menu ada di scene sama:
        GameManager.Instance.ChangeState(GameState.MainMenu);
 
        // Jika menu di scene terpisah, uncomment ini:
        // SceneManager.LoadScene("MainMenu");
    }
 
    // ----- Tombol Quit -----
    public void QuitGame()
    {
        Application.Quit();
    }
}