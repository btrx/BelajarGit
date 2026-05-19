using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject PauseMenu;

    public static GameManager Instance;

    public GameState currentState;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentState = GameState.Playing;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentState == GameState.Playing){
                PauseGame();
            }
            else if (currentState == GameState.Paused){
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;
        PauseMenu.SetActive(true);
    }

    public void ResumeGame(){
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        PauseMenu.SetActive(false);
    }

    // public void GameOver()
    // {
    //     Debug.Log("Game Over");
    //     SceneManager.LoadScene("MainMenu");
    //     currentState = GameState.GameOver;
    //     Time.timeScale = 1f; 
    // }
}