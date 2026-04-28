using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currentState;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Fungsi ini yang tadi merah di PlayerController
    public void GameOver()
    {
        currentState = GameState.GameOver;
        Time.timeScale = 0f; // Berhentikan game
        Debug.Log("Game Over Cuy!");
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }
}