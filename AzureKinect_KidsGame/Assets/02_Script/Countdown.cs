using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public TMP_Text countdownText;
    public Image countdownImage;
    public AudioSource countdownAudio; 

    //public AudioClip countThreeSound;
    //public AudioClip countTwoSound; 
    //public AudioClip countOneSound; 
    //public AudioClip goSound; 

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
        //countdownAudio.PlayOneShot(countThreeSound);
        countdownImage.gameObject.SetActive(true);
        startTime = Time.realtimeSinceStartup;
        yield return new WaitForSecondsRealtime(1);

        countdownText.text = "2";
        //countdownAudio.PlayOneShot(countTwoSound); 
        yield return new WaitForSecondsRealtime(1);

        countdownText.text = "1";
        //countdownAudio.PlayOneShot(countOneSound); 
        yield return new WaitForSecondsRealtime(1);

        countdownText.text = "GO!";
        //countdownAudio.PlayOneShot(goSound); 
        yield return new WaitForSecondsRealtime(1);

        countdownText.gameObject.SetActive(false);
        countdownImage.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
