using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
public class ToggleSoundEffectWithCounter : MonoBehaviour
{
    [Header("UI & Audio")]
    public Toggle toggle;
    public AudioSource audioSource;
    public AudioClip postThresholdSound;
    [Header("Easter Egg Settings")]
    public string youtubeURL = "https://www.youtube.com/@ChairThatSpins";
    public float timeLimit = 10f;
    public int clickThreshold = 20;
    [Header("Image Fade Settings")]
    public CanvasGroup imageCanvasGroup;
    public GameObject imageObject;
    public float fadeDuration = 1f;
    public float displayDuration = 3f;

    private int clickCount = 0;
    private float timer = 0f;
    private bool counting = false;
    private bool linkOpened = false;
    void Start()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(OnToggleChanged);
        }
        if (imageObject != null)
        {
            imageObject.SetActive(false);
        }
    }
    void OnToggleChanged(bool isOn)
    {
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
        if (clickCount >= clickThreshold && !linkOpened)
        {
            linkOpened = true;
            StartCoroutine(TriggerEasterEgg());
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
        clickCount = 0;
        linkOpened = false;
    }
    IEnumerator TriggerEasterEgg()
    {
        OpenBrowser(youtubeURL);

        if (audioSource != null && postThresholdSound != null)
        {
            audioSource.PlayOneShot(postThresholdSound);
        }
        if (imageCanvasGroup != null && imageObject != null)
        {
            imageObject.SetActive(true);
            yield return StartCoroutine(FadeCanvasGroup(imageCanvasGroup, 0f, 1f, fadeDuration));
            yield return new WaitForSeconds(displayDuration);
            yield return StartCoroutine(FadeCanvasGroup(imageCanvasGroup, 1f, 0f, fadeDuration));
            imageObject.SetActive(false);
        }
    }
    IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cg.alpha = end;
    }
    void OpenBrowser(string url)
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        try
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        catch
        {
            Application.OpenURL(url);
        }
#else
        Application.OpenURL(url);
#endif
    }
}