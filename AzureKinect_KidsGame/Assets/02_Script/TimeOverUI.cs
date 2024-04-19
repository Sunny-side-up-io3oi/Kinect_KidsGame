using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeOverUI : MonoBehaviour
{
    public TMP_Text timerTextR;
    public TMP_Text timerTextL;
    // public Slider timeSlider;
    public GameObject timeOverUI; 
    private float timer = 90f;
    private bool sceneSwitched = false;

    void Update()
    {
        /*if (sceneSwitched)
            return;
        */
        timer -= Time.deltaTime;
        
        UpdateTimerUI();

        if (timer <= 0)
        {
            //SwitchScene();
            //sceneSwitched = true;
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timerTextR.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerTextL.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    
    public void TimeOver()
    {
        
        timeOverUI.SetActive(true);
        
    }
}
