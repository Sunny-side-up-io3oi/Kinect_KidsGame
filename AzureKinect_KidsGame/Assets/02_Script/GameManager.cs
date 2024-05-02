using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI scoreLText;
    public TextMeshProUGUI bestscoreText;
    public int score { get; private set; }
    //public int scoreL { get; private set; }
    public int bestscore { get; private set; }
    private bool isGameOver = false;
    public TMP_Text timerTextR;
    //public TMP_Text timerTextL;
    public Slider timeSlider;
    public GameObject bestScoreUI;
    public Button returnToMainButton;
    //public AudioClip scoreClip; // 득점 사운드 클립
    //private AudioSource audioSource;

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
        //audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기
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
            EndGame(); // 타이머가 끝나면 게임 종료
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timerTextR.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //timerTextL.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
            // 득점 사운드 재생
            //audioSource.PlayOneShot(scoreClip);
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
            //Debug.Log("Game Over");
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = " " + score.ToString();
        //scoreLText.text = "Score: " + score.ToString();
        bestscoreText.text = " " + score.ToString();
    }

    public AudioSource scoreSound;

    public void PlayScoreSound()
    {
        if (scoreSound != null && !scoreSound.isPlaying)
        {
            scoreSound.Play();
        }
    }
}
