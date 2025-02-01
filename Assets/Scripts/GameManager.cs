using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float survivalTime = 30f;
    private float timer;
    private bool isGameOver;
    public bool IsGamePaused { get; private set; } = false;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWinUI;
    [SerializeField] private Image timerImage;

    private void Awake()
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (!isGameOver && !IsGamePaused)
        {
            timer += Time.deltaTime;
            UpdateTimerImage();

            if (timer >= survivalTime)
            {
                GameWin();
            }
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameWin()
    {
        isGameOver = true;
        gameWinUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        IsGamePaused = !IsGamePaused;
        Time.timeScale = IsGamePaused ? 0 : 1;
    }

    public void ResumeGame()
    {
        IsGamePaused = false;
        Time.timeScale = 1;
    }

    private void UpdateTimerImage()
    {
        if (timerImage != null)
        {
            timerImage.fillAmount = 1 - (timer / survivalTime);
        }
    }
}
