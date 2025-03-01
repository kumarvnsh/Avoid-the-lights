using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the main menu, including starting the game, showing 
/// level selection, and quitting.
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("Level Selection Panel")]
    [SerializeField] private GameObject levelSelectionPanel;

    private void Start()
    {
        // Hide level selection panel if it exists
        if (levelSelectionPanel != null)
        {
            levelSelectionPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Shows the level selection panel.
    /// </summary>
    public void OnStartButtonClick()
    {
        if (levelSelectionPanel != null)
        {
            levelSelectionPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Hides the level selection panel (back button).
    /// </summary>
    public void OnBackButtonClick()
    {
        if (levelSelectionPanel != null)
        {
            levelSelectionPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Loads a level/scene by name.
    /// </summary>
    public void LoadLevel(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Invalid scene name provided.");
        }
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
