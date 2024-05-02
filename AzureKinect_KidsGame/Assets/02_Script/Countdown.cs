using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Sprite[] countdownSprites;
    public Image countdownImage;
    public Image countdownObject; 

    public AudioSource countdownAudio;
    public AudioClip countThreeSound;
    public AudioClip countTwoSound;
    public AudioClip countOneSound;
    public AudioClip goSound;

    private void Start()
    {
        if (countdownSprites != null && countdownSprites.Length > 0 && countdownImage != null)
        {
            Time.timeScale = 0f;
            StartCoroutine(StartGame());
        }
    }

    private IEnumerator StartGame()
    {
        for (int i = 0; i < countdownSprites.Length; i++)
        {
            countdownImage.sprite = countdownSprites[i];
            countdownObject.gameObject.SetActive(true);

            PlayCountdownSound(i + 1);

            yield return new WaitForSecondsRealtime(1);
        }

        countdownObject.gameObject.SetActive(false); 
        Time.timeScale = 1f;
    }

    private void PlayCountdownSound(int count)
    {
        switch (count)
        {
            case 1:
                if (countdownAudio != null && countOneSound != null)
                    countdownAudio.PlayOneShot(countOneSound);
                break;
            case 2:
                if (countdownAudio != null && countTwoSound != null)
                    countdownAudio.PlayOneShot(countTwoSound);
                break;
            case 3:
                if (countdownAudio != null && countThreeSound != null)
                    countdownAudio.PlayOneShot(countThreeSound);
                break;
            default:
                break;
        }
    }
}
