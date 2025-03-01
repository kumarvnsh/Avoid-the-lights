using UnityEngine;

/// <summary>
/// Controls the pause menu UI and toggles the paused state through the GameManager.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [Header("Menu UI")]
    public GameObject pauseMenuUI;

    private void Update()
    {
        // Press Escape to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.IsGamePaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    /// <summary>
    /// Resumes the game from paused state.
    /// </summary>
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameManager.Instance?.TogglePause();
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameManager.Instance?.TogglePause();
    }

    /// <summary>
    /// Loads the main menu scene if needed.
    /// </summary>
    public void LoadMainMenu()
    {
        // Resume time before loading a new scene
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
