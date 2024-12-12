using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float survivalTime = 30f; // Time player needs to survive
    private float timer;
    private bool isGameOver;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWinUI;
    [SerializeField] private Image timerImage; // Reference to the timer UI Image

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

        if (!isGameOver)
        {
            timer += Time.deltaTime;

            // Update the timer image fill
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
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    private void UpdateTimerImage()
    {
        if (timerImage != null)
        {
            timerImage.fillAmount = 1 - (timer / survivalTime); // Scale timer to fill amount
        }
    }
}
