using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [System.Serializable]
    public class Player
    {
        public string playerName; // 플레이어 이름 속성
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI bestScoreText;
        public int score;
        public int bestScore;
    }

    public Player[] players;

    public TMP_Text timerText;
    public Slider timeSlider;
    public Button returnToMainButton;
    public AudioSource scoreSound;
    public GameObject TimeoverUIPrefab;

    private float totalTime = 31f;
    private float timer;
    private bool timerStopped = false;
    private bool isGameOver = false;

    public bool IsGameOver { get { return isGameOver; } }

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

    private void Start()
    {
        timer = totalTime;
        timeSlider.maxValue = totalTime;
        returnToMainButton.gameObject.SetActive(false);
        InitializePlayers();
    }

    private void Update()
    {
        if (timerStopped)
            return;

        timer -= Time.deltaTime;
        timer = Mathf.Max(timer, 0);

        UpdateTimerUI();
        UpdateSliderValue();

        if (timer <= 0)
        {
            timer = 0;
            timerStopped = true;
            EndGame();
        }
    }

    private void InitializePlayers()
    {
        foreach (Player player in players)
        {
            player.score = 0;
            player.bestScore = 0;
            UpdatePlayerUI(player);
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateSliderValue()
    {
        timeSlider.value = totalTime - timer;
    }

    public void AddScore(string playerName, int value)
    {
        if (!isGameOver)
        {
            Player player = FindPlayer(playerName);
            if (player != null)
            {
                player.score += value;
                if (player.score > player.bestScore)
                {
                    player.bestScore = player.score;
                }

                UnityEngine.Debug.Log($"Player {playerName} scored. New Score: {player.score}, Best Score: {player.bestScore}");

                UpdatePlayerUI(player);
                PlayScoreSound();
            }
            else
            {
                UnityEngine.Debug.LogWarning($"Player {playerName} not found.");
            }
        }
    }

    private Player FindPlayer(string playerName)
    {
        // (Clone)을 제거한 이름으로 비교
        string cleanedPlayerName = playerName.Replace("(Clone)", "").Trim();

        foreach (Player player in players)
        {
            if (player.playerName == cleanedPlayerName)
            {
                return player;
            }
        }
        return null;
    }

    private void UpdatePlayerUI(Player player)
    {
        player.scoreText.text = " " + player.score;
        player.bestScoreText.text = " " + player.bestScore;
    }

    public void EndGame()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Time.timeScale = 0;
            foreach (Player player in players)
            {
                UpdatePlayerUI(player);
            }
            // Timeover UI 활성화
            returnToMainButton.gameObject.SetActive(true);
            TimeoverUIPrefab.gameObject.SetActive(true);
        }
    }


    public void PlayScoreSound()
    {
        if (scoreSound != null && !scoreSound.isPlaying)
        {
            scoreSound.Play();
        }
    }
}
