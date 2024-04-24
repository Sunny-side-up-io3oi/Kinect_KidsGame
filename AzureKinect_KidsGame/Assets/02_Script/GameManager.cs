using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
/*
public enum GameState
{
    Ready,
    Start,
    TimeOver
}

public class GameManager : MonoBehaviour
{
    public TMP_Text gameStateText;
    public GameObject countdownUI;
    public Image countdownImage;
    public Sprite[] countdownSprites;
    public TMP_Text timerTextR;
    public TMP_Text timerTextL;
    public Slider timeSlider;
    public GameObject timeOverUI;
    private float totalTime = 10f;
    private float timer;
    private bool timerStopped = false; 
    private int countdownValue = 3;
    private float countdownDuration = 1f;

    void Start()
    {
        timer = totalTime;
        timeSlider.maxValue = totalTime;
        timeOverUI.SetActive(false); 
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        countdownUI.SetActive(true); 
        yield return new WaitForSeconds(3f);

        SetGameState(GameState.Start); 

        while (countdownValue > 0)
        {
            countdownImage.sprite = countdownSprites[countdownValue - 1]; 
            countdownValue--;
            yield return new WaitForSeconds(countdownDuration);
        }

        countdownUI.SetActive(false); 
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
            TimeOver();
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

    void SetGameState(GameState state)
    {
        //gameState = state;

        switch (state)
        {
            case GameState.Ready:
                gameStateText.text = "Ready";
                break;
            case GameState.Start:
                gameStateText.text = "Start";
                break;
            case GameState.TimeOver:
                gameStateText.text = "Time Over";
                break;
        }
    }

    public void TimeOver()
    {
        SetGameState(GameState.TimeOver);
        timeOverUI.SetActive(true); 
        
    }
}*/





public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI scoreText; // 점수를 표시할 텍스트
    public int score { get; private set; } // 현재 점수
    private bool isGameOver = false; // 게임 종료 여부

    public bool IsGameOver { get { return isGameOver; } }

    private void Awake()
    {
        // 싱글톤 패턴 적용
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
        UpdateScoreText(); // 게임 시작시 텍스트 업데이트
    }

    public void AddScore(int value)
    {
        if (!isGameOver)
        {
            score += value;
            UpdateScoreText(); // 점수 변경시 텍스트 업데이트
        }
    }

    public void EndGame()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("Game Over");
            // 여기에 게임 종료 처리 코드 추가
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // 텍스트 업데이트
    }
}


