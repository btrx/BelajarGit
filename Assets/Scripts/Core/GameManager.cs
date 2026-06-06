using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameState currentState;

        [Header("UI Panel")]
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject gameOverPanel;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            ChangeState(GameState.Playing);

            pausePanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }

        void Update()
        {
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

        public void ChangeState(GameState newState)
        {
            currentState = newState;

            switch (newState)
            {
                case GameState.Playing:
                    Time.timeScale = 1f;

                    pausePanel.SetActive(false);
                    gameOverPanel.SetActive(false);
                    break;

                case GameState.Paused:
                    Time.timeScale = 0f;

                    pausePanel.SetActive(true);
                    break;

                case GameState.GameOver:
                    Time.timeScale = 0f;

                    gameOverPanel.SetActive(true);
                    break;
            }
        }

        public void PauseGame()
        {
            ChangeState(GameState.Paused);
        }

        public void ResumeGame()
        {
            ChangeState(GameState.Playing);
        }

        public void GameOver()
        {
            ChangeState(GameState.GameOver);
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }

        public void BackToMenu()
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene("MainMenu");
        }
    }