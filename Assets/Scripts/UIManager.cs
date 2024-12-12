using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void OnPauseButtonClick()
    {
        GameManager.Instance.PauseGame();
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void OnRestartButtonClick()
    {
        GameManager.Instance.RestartGame();
    }
}
