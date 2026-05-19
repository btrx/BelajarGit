using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;
    
  public GameObject canvasPause;	
  public GameObject canvasGameOver;

  

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentState = GameState.Playing;
        canvasPause.SetActive(false);
        canvasGameOver.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
                PauseGame();
            else if (currentState == GameState.Paused)
                PlayGame();
        }
        
    }
    
     public void PlayGame()
    {
        Debug.Log("game resumed");
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        canvasPause.SetActive(false);
    }
   
    public void PauseGame()
    {
        Debug.Log("game paused");
        Time.timeScale = 0f;
        currentState = GameState.Paused;
        canvasPause.SetActive(true);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
        currentState = GameState.GameOver;
        canvasGameOver.SetActive(true);
    }
     public void KeMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }



    

    
}