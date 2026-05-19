using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public void ClickResume()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateState(GameState.Playing);
        }
    }

    public void ClickMainMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GoToMainMenu();
        }
    }
}