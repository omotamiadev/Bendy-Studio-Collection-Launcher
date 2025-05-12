using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ToggleSoundEffectWithCounter : MonoBehaviour
{
    public Toggle toggle; 
    public AudioSource audioSource; 
    public AudioClip enabledSound; 
    public AudioClip disabledSound; 
    public string youtubeURL = "https://www.youtube.com/@ChairThatSpins"; 
    public float timeLimit = 10f;
    public int clickThreshold = 20; 
    private int clickCount = 0;
    private float timer = 0f;
    private bool counting = false;
    void Start()
    {
        toggle.onValueChanged.AddListener(PlayToggleSound);
    }
    void PlayToggleSound(bool isOn)
    {
        if (isOn)
        {
            audioSource.PlayOneShot(enabledSound);
        }
        else
        {
            audioSource.PlayOneShot(disabledSound);
        }
        CountClicks();
    }
    void CountClicks()
    {
        if (!counting)
        {
            counting = true;
            clickCount = 0;
            timer = timeLimit;
            StartCoroutine(ClickTimer());
        }
        clickCount++;
        if (clickCount > clickThreshold)
        {
            OpenYouTubeLink();
        }
    }
    IEnumerator ClickTimer()
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        counting = false;
    }
    void OpenYouTubeLink()
    {
        Application.OpenURL(youtubeURL);
    }
}