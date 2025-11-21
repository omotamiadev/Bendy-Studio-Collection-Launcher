using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
public class BATIMswap : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Toggle epicToggle;
    [SerializeField] private Button openButton;
    [Header("Steam Settings")]
    [SerializeField] private string steamAppID = "622650";
    [Header("Epic Settings")]
    private string epicUri = "com.epicgames.launcher://apps/4ebf21b7761e40cdb180d0604127e4a6%3A3a671a15148746999d9f5a010f0ba4a3%3A39700e23ec114d7581aedea23779ce2d?action=launch&silent=true";
    private const string EpicPrefKey = "EpicToggleState";

    private void Awake()
    {
        if (epicToggle != null)
        {
            bool savedState = PlayerPrefs.GetInt(EpicPrefKey, 0) == 1;
            epicToggle.isOn = savedState;
            epicToggle.onValueChanged.AddListener(OnEpicToggleChanged);
        }

        if (openButton != null)
        {
            openButton.onClick.AddListener(LaunchBATIM);
        }
    }
    private void OnDestroy()
    {
        if (openButton != null)
        {
            openButton.onClick.RemoveListener(LaunchBATIM);
        }
        if (epicToggle != null)
        {
            epicToggle.onValueChanged.RemoveListener(OnEpicToggleChanged);
        }
    }
    private void OnEpicToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt(EpicPrefKey, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
    private void LaunchBATIM()
    {
        if (epicToggle != null && epicToggle.isOn)
        {
            OpenEpic();
        }
        else
        {
            OpenSteam();
        }
    }
    private void OpenSteam()
    {
        string steamUri = $"steam://run/{steamAppID}";
        Process.Start(new ProcessStartInfo(steamUri) { UseShellExecute = true });
        Application.Quit();
    }
    private void OpenEpic()
    {
        Process.Start(new ProcessStartInfo(epicUri) { UseShellExecute = true });
        Application.Quit();
    }
}