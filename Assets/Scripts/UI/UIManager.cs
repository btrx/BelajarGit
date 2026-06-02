using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        // Pindah ke scene index 1 (asumsinya Game ada di urutan kedua)
        // Cek Build Profiles: Kalau Game indexnya 0, ganti jadi 0.
        SceneManager.LoadScene(0); 
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Keluar!");
    }
}