using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // biar play dari main menu
        ChangeState(GameState.MainMenu);
    }

    void Update()
    {
        // Logika Pause menggunakan Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                PauseGame();
            }
            else if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
        }
    }

    // Fungsi tambahan biar gonta ganti lah pokoknya
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        
        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1f;
                break;
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            case GameState.GameOver:
                Time.timeScale = 0f; //  Waktu berhenti saat kalah
                break;
        }
    }

    public void PauseGame()
    {
        ChangeState(GameState.Paused);
    }

    // Fungsi tambahan agar player bisa main lagi setelah pause
    public void ResumeGame()
    {
        ChangeState(GameState.Playing);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        ChangeState(GameState.GameOver);
    }
}

// udahan ah, besok ajah 