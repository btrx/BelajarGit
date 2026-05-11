using UnityEngine;
using UnityEngine.SceneManagement;
 
public class UIManager : MonoBehaviour
{
   
    public void StartGame()
    {
        
        GameManager.Instance.ChangeState(GameState.Playing);
 
       
    }
 
    
    public void Pause()
    {
        if (GameManager.Instance.currentState == GameState.Playing)
        {
            GameManager.Instance.ChangeState(GameState.Pause);
        }
    }
 
    public void Resume()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
    }
 
    
    public void Restart()
    {
        Time.timeScale = 1f; // pastikan timeScale normal sebelum reload
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
 
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
       
        GameManager.Instance.ChangeState(GameState.MainMenu);
 
       
    }
 
    
    public void QuitGame()
    {
        Application.Quit();
    }
}