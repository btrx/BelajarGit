using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.StartGame();
        SceneManager.LoadScene("Game");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        GameManager.Instance.StartGame();
        SceneManager.LoadScene("Game");
    }
}