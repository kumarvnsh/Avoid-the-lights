using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectionPanel; // Panel for level selection

    private void Start()
    {
        // Ensure the level selection panel is hidden on start
        if (levelSelectionPanel != null)
            levelSelectionPanel.SetActive(false);

            
    }

    // Function to handle the Start button click
    public void OnStartButtonClick()
    {
        if (levelSelectionPanel != null)
        {
            levelSelectionPanel.SetActive(true); // Show the level selection panel
        }
    }

    // Function to handle level selection
    public void LoadLevel(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName); // Load the scene by name
        }
        else
        {
            Debug.LogWarning("Invalid scene name provided.");
        }
    }

    // Function to quit the application
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
