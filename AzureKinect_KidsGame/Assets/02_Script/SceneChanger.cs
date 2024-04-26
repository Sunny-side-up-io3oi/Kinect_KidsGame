using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip buttonClickSound;


    public void MainSceneChange()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene("MainScene");
    }

    public void StartSceneChange()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene("Start");
    }

    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound); 
        }
    }
}
