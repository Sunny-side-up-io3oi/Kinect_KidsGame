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
    public Sprite[] countdownSprites; // 0: 3, 1: 2, 2: 1
    public TMP_Text timerTextR;
    public TMP_Text timerTextL;
    public Slider timeSlider;
    public GameObject timeOverUI;
    private float totalTime = 10f;
    private float timer;
    private bool timerStopped = false; // 타이머가 멈춘 상태인지 여부를 나타내는 변수
    private int countdownValue = 3;
    private float countdownDuration = 1f;

    void Start()
    {
        timer = totalTime;
        timeSlider.maxValue = totalTime;
        timeOverUI.SetActive(false); // 처음에 TimeOverUI를 비활성화합니다.
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        countdownUI.SetActive(true); // 카운트 다운 UI 활성화

        yield return new WaitForSeconds(3f); // 3초 대기

        SetGameState(GameState.Start); // 게임 시작 상태로 변경

        while (countdownValue > 0)
        {
            countdownImage.sprite = countdownSprites[countdownValue - 1]; // 숫자 이미지 업데이트
            countdownValue--;
            yield return new WaitForSeconds(countdownDuration);
        }

        countdownUI.SetActive(false); // 카운트 다운 UI 비활성화
    }

    void Update()
    {
        if (timerStopped) // 타이머가 멈추었다면 업데이트를 중지합니다.
            return;

        timer -= Time.deltaTime;
        timer = Mathf.Max(timer, 0); // 타이머가 음수가 되지 않도록 보정합니다.

        UpdateTimerUI();
        UpdateSliderValue();

        if (timer <= 0)
        {
            timer = 0; // 타이머가 0 이하로 되지 않도록 설정합니다.
            timerStopped = true; // 타이머가 멈추도록 플래그를 설정합니다.
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
        timeSlider.value = totalTime - timer; // 슬라이더의 값은 전체 시간에서 현재 시간을 뺀 값입니다.
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
        SetGameState(GameState.TimeOver); // 타임 오버 상태로 변경
        timeOverUI.SetActive(true); // 타이머가 종료되면 TimeOverUI를 활성화합니다.
        // 여기에 타이머가 멈추었을 때 수행할 작업을 추가할 수 있습니다.
    }
}
