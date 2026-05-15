using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { MainMenu, Playing, Pause, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currentState;

    public GameObject MainMenu;
    public GameObject PauseMenu;
    public GameObject GameOverMenu;


    void Awake()
    {
      if (Instance == null) Instance = this;
    }

    void Start()
    {
        ChangeState(GameState.MainMenu);
    }
    

    public void ChangeState(GameState newstate)
    {
        currentState = newstate;

        
        if (MainMenu != null) MainMenu.SetActive(newstate == GameState.MainMenu);
        if (PauseMenu != null) PauseMenu.SetActive(newstate == GameState.Pause);
        if (GameOverMenu != null) GameOverMenu.SetActive(newstate == GameState.GameOver);

        Time.timeScale = (newstate == GameState.Pause) ? 0f : 1f;
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}