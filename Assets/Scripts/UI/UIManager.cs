// using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        // Untuk Keluar dari game
        Debug.Log("Tombol quit ke klik");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}