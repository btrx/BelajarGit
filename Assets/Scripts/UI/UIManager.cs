using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.currentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        GameManager.Instance.currentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }
}