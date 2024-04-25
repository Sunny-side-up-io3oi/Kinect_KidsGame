using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class Countdown : MonoBehaviour
{
    public TMP_Text countdownText;
    public Image countdownImage;
    private float startTime;

    private void Start()
    {
        if (countdownText != null)
        {
            Time.timeScale = 0f;
            StartCoroutine(StartGame());
        }
    }

    private IEnumerator StartGame()
    {
        countdownText.text = "3";
        countdownImage.gameObject.SetActive(true); 
        startTime = Time.realtimeSinceStartup;
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1);
        countdownText.gameObject.SetActive(false);
        countdownImage.gameObject.SetActive(false); 
        Time.timeScale = 1f;
    }
}
