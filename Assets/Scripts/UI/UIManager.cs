using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("aaaas");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}