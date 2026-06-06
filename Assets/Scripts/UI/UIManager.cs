using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Resume()
    {
        GameManager.Instance.ResumeGame();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }

    public void QuitGame()
    {
        Debug.Log("SISTEM: Tombol Quit Berhasil Diklik!");

        #if UNITY_EDITOR
            // Menghentikan mode Play di Editor
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Menutup aplikasi (.exe / .apk)
            Application.Quit();
        #endif
    }

    public void Restart()
    {
        GameManager.Instance.RestartGame();
    }
    
}