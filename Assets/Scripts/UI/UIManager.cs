using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject PanelMenu;
    public GameObject PanelPause;
    public GameObject PanelGameOver;

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        GameState state = GameManager.Instance.currentState;

        PanelMenu.SetActive(state == GameState.MainMenu);
        PanelPause.SetActive(state == GameState.Paused);
        PanelGameOver.SetActive(state == GameState.GameOver);
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
        UpdateUI();
    }

    public void Restart()
    {
         StartCoroutine(RestartRoutine());
    }

    IEnumerator RestartRoutine()
    {
        Time.timeScale = 1f;

        GameManager.Instance.currentState = GameState.Playing;

        UpdateUI();

        yield return SceneManager.UnloadSceneAsync("Game");

        yield return SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);

        GameManager.Instance.currentState = GameState.Playing;

        UpdateUI();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}