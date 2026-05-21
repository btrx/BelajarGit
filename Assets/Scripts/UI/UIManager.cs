using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Mengurus referensi untuk Panel UI sesuai kebutuhan UAS
    [Header("UI Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
        // Pastikan panel tertutup rapi saat scene baru dimuat
        if (pausePanel != null) pausePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        // Pastikan timeScale dikembalikan ke 1 agar game tidak freeze setelah restart
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void ResumeGame()
    {
        // Memanggil fungsi resume dari GameManager kamu secara aman
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResumeGame();
        }
        
        // Sembunyikan pause panel setelah game jalan lagi
        if (pausePanel != null) pausePanel.SetActive(false);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // --- TAMBAHAN UNTUK UAS ---

    // Fungsi untuk memunculkan Panel Pause (bisa dipanggil dari GameManager)
    public void ShowPausePanel(bool isPaused)
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
        }
    }

    // Fungsi untuk memunculkan Panel Game Over (dipanggil saat player mati)
    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            
            // Membuka kursor mouse agar player bisa ngeklik tombol Restart/Menu
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}