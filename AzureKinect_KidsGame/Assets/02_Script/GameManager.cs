using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [System.Serializable]
    public class Player
    {
        public string playerName; // 플레이어 이름 속성 추가
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI bestscoreText;
        public int score;
        public int bestScore;
    }

    public Player[] players;

    public TMP_Text timerText;
    public Slider timeSlider;
    //public GameObject bestScoreUI1P;
    //public GameObject bestScoreUI2P;
    public Button returnToMainButton;
    public AudioSource scoreSound;

    private float totalTime = 91f;
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
        //bestScoreUI1P.SetActive(false);
        //bestScoreUI2P.SetActive(false);
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

                UpdatePlayerUI(player);
                PlayScoreSound();
            }
        }
    }

    private Player FindPlayer(string playerName)
    {
        foreach (Player player in players)
        {
            if (player.playerName == playerName)
            {
                return player;
            }
        }
        return null;
    }

    private void UpdatePlayerUI(Player player)
    {
        player.scoreText.text = " " + player.score;
        player.bestscoreText.text = "Best Score: " + player.bestScore;
    }

    public void EndGame()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Time.timeScale = 0;
            foreach (Player player in players)
            {
                player.score = 0;
                UpdatePlayerUI(player);
            }
            //bestScoreUI1P.SetActive(true);
            //bestScoreUI2P.SetActive(true);
            returnToMainButton.gameObject.SetActive(true);
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
