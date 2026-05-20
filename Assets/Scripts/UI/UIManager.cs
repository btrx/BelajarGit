using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // 1. Don't forget this!

public class UIManager : MonoBehaviour
{
    // 2. This must be INSIDE the class brackets
    [SerializeField] private TextMeshProUGUI buttonText;

    public void StartGame()
    {
        Time.timeScale = 1f; // Always reset time when starting/restarting
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Game");
    }
    
public void TogglePause()
{
    // Pengaman 1: Cek apakah GameManager ada di Scene
    if (GameManager.Instance == null)
    {
        Debug.LogError("Error: GameManager tidak ditemukan di Scene! Pastikan sudah ada GameObject dengan script GameManager.");
        return; 
    }

    // Pengaman 2: Cek apakah variabel buttonText sudah diisi di Inspector
    if (buttonText == null)
    {
        Debug.LogError("Error: Variabel 'buttonText' masih kosong di Inspector UIManager! Tolong di-drag dulu komponen Text-nya.");
        return; 
    }

    // --- KODE ASLI KAMU (Sekarang Aman dari NullReference) ---
    if (GameManager.Instance.currentState == GameState.Playing)
    {
        GameManager.Instance.PauseGame();
        buttonText.text = "Resume";
    }
    else if (GameManager.Instance.currentState == GameState.Paused)
    {
        GameManager.Instance.ResumeGame();
        buttonText.text = "Pause";
    }
}
}