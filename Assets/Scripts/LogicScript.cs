using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int playerScore = 0;
    public int highScore = 0;

    public Text scoreText;
    public Text highScoreText;       // UI Text hiển thị điểm cao nhất (gán trong Inspector)
    public GameObject startPanel;    // UI Panel khi bắt đầu trò chơi
    public GameObject gameOverPanel; // UI Panel khi Game Over

    public bool isGameStarted = false;
    public bool isGameOver = false;

    // Biến static giữ trạng thái tự động vào game sau khi Restart
    public static bool autoStartOnReload = false;

    private const string HIGH_SCORE_KEY = "HighScore";

    void Start()
    {
        // Tải điểm cao nhất lưu từ PlayerPrefs
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        UpdateHighScoreUI();

        // Kiểm tra xem đây là lần chơi lại (Restart) hay vừa bật game
        if (autoStartOnReload)
        {
            autoStartOnReload = false;
            if (startPanel != null) startPanel.SetActive(false);
            if (gameOverPanel != null) gameOverPanel.SetActive(false);
            StartGame();
        }
        else
        {
            if (startPanel != null) startPanel.SetActive(true);
            if (gameOverPanel != null) gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Nhấn phím Space để bắt đầu hoặc chơi lại
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isGameStarted && !isGameOver)
            {
                StartGame();
            }
            else if (isGameOver)
            {
                RestartGame();
            }
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        if (isGameOver || !isGameStarted) return;
        playerScore += scoreToAdd;

        if (scoreText != null)
        {
            scoreText.text = playerScore.ToString();
        }

        // Cập nhật High Score nếu vượt qua kỷ lục hiện tại
        if (playerScore > highScore)
        {
            highScore = playerScore;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
            PlayerPrefs.Save();
            UpdateHighScoreUI();
        }
    }

    public void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            // Chỉ cập nhật con số điểm để tránh bị lặp chuỗi "High Score: High Score:" 
            // nếu UI Text trên Unity đã được viết sẵn nhãn "High Score:"
            highScoreText.text = highScore.ToString();
        }
    }

    public void RestartGame()
    {
        autoStartOnReload = true; // Đánh dấu lần sau load lại scene sẽ tự động chơi luôn
        isGameStarted = false;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        isGameOver = true;

        // Lưu High Score khi thua nếu có kỷ lục mới
        if (playerScore > highScore)
        {
            highScore = playerScore;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
            PlayerPrefs.Save();
            UpdateHighScoreUI();
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Dừng toàn bộ chuyển động background trong scene
        BackgroundScript.StopAllBackgrounds();
    }
}
