using UnityEngine;

public class PauseInput : MonoBehaviour
{
    public GameObject pausePanel; // ← Drag PausePanel ke sini nanti

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc ditekan!");

            if (GameManager.Instance.currentState == GameManager.GameState.Playing)
            {
                GameManager.Instance.PauseGame();
                ShowPauseUI(true);
            }
            else if (GameManager.Instance.currentState == GameManager.GameState.Pause)
            {
                GameManager.Instance.ResumeGame();
                ShowPauseUI(false);
            }
        }
    }

    void ShowPauseUI(bool show)
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(show);
            Debug.Log("PausePanel aktif: " + show);
        }
        else
        {
            Debug.LogError("PausePanel tidak terhubung! Drag PausePanel ke script.");
        }
    }
}