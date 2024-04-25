using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreLText;
    public TextMeshProUGUI bestscoreText;
    public int score { get; private set; }
    public int scoreL { get; private set; }
    public int bestscore { get; private set; }
    private bool isGameOver = false;
    public TMP_Text timerTextR;
    public TMP_Text timerTextL;
    public Slider timeSlider;
    public GameObject bestScoreUI;
    public Button returnToMainButton;
    private float totalTime = 91f;
    private float timer;
    private bool timerStopped = false;

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
        UpdateScoreText();
        timer = totalTime;
        timeSlider.maxValue = totalTime;
        bestScoreUI.SetActive(false);
        returnToMainButton.gameObject.SetActive(false);
        //StartCoroutine(StartCountdown());
    }

    void Update()
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

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timerTextR.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerTextL.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void UpdateSliderValue()
    {
        timeSlider.value = totalTime - timer;
    }

    public void AddScore(int value)
    {
        if (!isGameOver)
        {
            score += value;
            UpdateScoreText();
        }
    }

    public void EndGame()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Time.timeScale = 0; 
            bestScoreUI.SetActive(true); 
            returnToMainButton.gameObject.SetActive(true);
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        scoreLText.text = "Score: " + score.ToString();
        bestscoreText.text = "Score: " + score.ToString();
    }
}
