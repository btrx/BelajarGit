using UnityEngine;
using UnityEngine.SceneManagement; // Untuk merestart game

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject tombolPlay;
    public GameObject panelGameOver;
    public Transform bola;
    
    private Vector3 posisiAwalBola;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (bola != null) posisiAwalBola = bola.position;
        if (panelGameOver != null) panelGameOver.SetActive(false);
        if (tombolPlay != null) tombolPlay.SetActive(true);
    }

    // Fungsi dipanggil saat tombol Play diklik
    public void GameDimulai()
    {
        if (tombolPlay != null) tombolPlay.SetActive(false); // Sembunyikan tombol Play
    }

    // Fungsi dipanggil ketika bola keluar batas
    public void TriggerGameOver()
    {
        if (panelGameOver != null) panelGameOver.SetActive(true); // Memunculkan Game Over
        
        // Hentikan gerakan bola
        Rigidbody2D rb = bola.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }

    // Fungsi dipanggil oleh tombol Restart/Resume untuk mengulang
    public void RestartGame()
    {
        // Memuat ulang scene yang sedang aktif agar kembali bersih
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
private bool isPaused = false;

public void TogglePause()
{
    isPaused = !isPaused;

    if (isPaused)
    {
        Time.timeScale = 0f; // Menghentikan seluruh waktu di game (Freeze)
    }
    else
    {
        Time.timeScale = 1f; // Menjalankan kembali game normal
    }
}

}