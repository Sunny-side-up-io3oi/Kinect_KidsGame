using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeOverSceneChange : MonoBehaviour
{
    public TMP_Text timerText; 
    private float timer = 180f; 
    private bool sceneSwitched = false;

    void Update()
    {
        
        if (sceneSwitched)
            return;

        
        timer -= Time.deltaTime;

        
        UpdateTimerUI();

       
        if (timer <= 0)
        {
            SwitchScene();
            sceneSwitched = true;
        }
    }

    void UpdateTimerUI()
    {
        
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

       
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void SwitchScene()
    {
       
        string nextSceneName = "Finish";
        SceneManager.LoadScene(nextSceneName); 
    }
}
