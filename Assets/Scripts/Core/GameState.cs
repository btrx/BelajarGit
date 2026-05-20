using UnityEngine;
using UnityEngine.Events;


public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}


// public class GameManager : MonoBehaviour
// {
//     public static GameManager Instance;

//     public GameState CurrentState { get; private set; }

//     public UnityEvent<GameState> OnGameStateChanged;

//     // Start is called once before the first execution of Update after the MonoBehaviour is created

//     void Awake()
//     {
//         if (Instance == null) Instance = this;
//         else Destroy(gameObject);
//     }

//     void Start()
//     {
//         Debug.Log("GameManager" + CurrentState);

//         UpdateState(GameState.MainMenu);

//     }

//     // Update is called once per frame
//     public void UpdateState(GameState newState)
//     {
//         CurrentState = newState;

//         switch (newState)
//         {
//             case GameState.MainMenu:
//                 HandleMainMenu();
//                 break;
//             case GameState.Playing:
//                 HandlePlaying();
//                 break;
//             case GameState.Paused:
//                 HandlePaused();
//                 break;
//             case GameState.GameOver:
//                 HandleGameOver();
//                 break;
//         }

//         OnGameStateChanged?.Invoke(newState);
//     }

//     private void HandleMainMenu()
//     {
//         Time.timeScale = 1f;
//         Debug.Log("Back to Main Menu");
//     }

//     private void HandlePlaying()
//     {
//         Time.timeScale = 1f; // Ensure game logic is running
//         Debug.Log("Game Started/Resumed");
//     }

//     private void HandlePaused()
//     {
//         Time.timeScale = 0f; // Freezes physics and frame-rate dependent logic
//         Debug.Log("Game Paused");
//     }

//     private void HandleGameOver()
//     {
//         Time.timeScale = 0f;
//         Debug.Log("Game Over!");
//     }

//     // Shortcut methods for UI Buttons
//     public void StartGame() => UpdateState(GameState.Playing);
//     public void PauseGame() => UpdateState(GameState.Paused);
//     public void ResumeGame() => UpdateState(GameState.Playing);
//     public void QuitGame() 
//     {
//         Debug.Log("Quitting App...");
//         Application.Quit(); 
//     }
// }