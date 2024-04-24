using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

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
}
