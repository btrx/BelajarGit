using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject PanelMenu;
    public GameObject PanelPause;
    public GameObject PanelGameOver;

    public void StartGame()
    {
        GameManager.Instance.StartGame();
        PanelMenu.SetActive(false);
    }

    void Update()
    {
        PanelPause.SetActive(GameManager.Instance.currentState == GameState.Paused);
        PanelGameOver.SetActive(GameManager.Instance.currentState == GameState.GameOver);
   }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameManager.Instance.currentState = GameState.Playing;

        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
       
    }
}