using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameManager.Instance.PauseGame();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.ResumeGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
