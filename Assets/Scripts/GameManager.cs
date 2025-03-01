using UnityEngine;

/// <summary>
/// The main game manager handling game state, pausing,
/// and overall gameplay flow.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game State UI")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWinUI;

    public bool IsGamePaused { get; private set; }

    [Header("Timer Settings")]
    [SerializeField] private float startTime = 30f;   // The total time you want
    [SerializeField] private UnityEngine.UI.Image timerImage;  // Assign in the Inspector

    private float timeRemaining;
    private bool isTimerActive = true;


    private void Awake()
    {
        // Singleton pattern for GameManager instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize the timer
        timeRemaining = startTime;
        if (timerImage != null)
        {
            timerImage.fillAmount = 1f;  // Fill is full at start
        }
    }

    private void Update()
    {
        // If game is paused or over, don’t run the timer
        if (IsGamePaused || !isTimerActive)
            return;

        // Decrease timeRemaining
        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(timeRemaining, 0f);

        // Update the timer fill
        if (timerImage != null)
        {
            timerImage.fillAmount = timeRemaining / startTime;
        }

        // If the timer hits zero, we can do something like end the game
        if (timeRemaining <= 0 && isTimerActive)
        {
            // Mark timer as done
            isTimerActive = false;
             GameWin();
        }
    }



    /// <summary>
    /// Called when the player's health reaches 0.
    /// Displays game over UI and stops time.
    /// </summary>
    public void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        Time.timeScale = 0;
    }

    /// <summary>
    /// Called when the timer or conditions for winning are met.
    /// Shows the win UI and stops time.
    /// </summary>
    public void GameWin()
    {
        if (gameWinUI != null)
        {
            gameWinUI.SetActive(true);
        }
        Time.timeScale = 0;
    }

    /// <summary>
    /// Toggle the pause state of the game.
    /// </summary>
    public void TogglePause()
    {
        IsGamePaused = !IsGamePaused;
        Time.timeScale = IsGamePaused ? 0 : 1;
    }

    /// <summary>
    /// restart the level.
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

}
