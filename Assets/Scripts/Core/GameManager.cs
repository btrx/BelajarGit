using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject overCanvas;
    void Awake()
    {
        Instance = this;
        // pauseCanvas.SetActive(false);
        // overCanvas.SetActive(false);
    }

    void Start()
    {
        currentState = GameState.Playing;
        pauseCanvas.SetActive(false);
        overCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)  && currentState == GameState.Playing)
        {
            PauseGame();

        }
        else if (Input.GetKeyDown(KeyCode.Escape)  && currentState == GameState.Paused)
        {
            currentState = GameState.Playing;
            Time.timeScale = 1f;
            Debug.Log("Game Continue");
            pauseCanvas.SetActive(false);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;
        Debug.Log("Game Pause");
        pauseCanvas.SetActive(true);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        currentState = GameState.GameOver;
        Time.timeScale = 0f;
        overCanvas.SetActive(true);
    }
}