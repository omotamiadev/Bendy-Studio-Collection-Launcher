using System.Diagnostics;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ExeLauncher : MonoBehaviour
{
    public Button[] launchButtons;
    public string[] exePaths = new string[6];
    public bool[] quitOnLaunch = new bool[6];
    public Button altLaunchButton;
    public string altExePath;
    private bool useAltVersion = false;
    void Start()
    {
        for (int i = 0; i < exePaths.Length; i++)
        {
            if (launchButtons.Length > i)
            {
                int buttonIndex = i;
                if (buttonIndex == 3)
                {
                    launchButtons[i].onClick.AddListener(() => StartCoroutine(OpenExeOrStore(buttonIndex)));
                    altLaunchButton.onClick.AddListener(() => StartCoroutine(OpenExeOrStore(buttonIndex)));
                }
                else
                {
                    launchButtons[i].onClick.AddListener(() => StartCoroutine(OpenExeOrStore(buttonIndex)));
                }
            }
        }
        altLaunchButton.gameObject.SetActive(false);
    }
    public void ToggleAltVersion()
    {
        useAltVersion = !useAltVersion;
        launchButtons[3].gameObject.SetActive(!useAltVersion);
        altLaunchButton.gameObject.SetActive(useAltVersion);
        UnityEngine.Debug.Log($"Alt version toggled: {useAltVersion}");
    }
    IEnumerator OpenExeOrStore(int index)
    {
        if (index >= exePaths.Length)
        {
            UnityEngine.Debug.LogError($"Invalid index {index}: exePaths size is {exePaths.Length}");
            yield break;
        }
        string targetPath = (index == 3 && useAltVersion) ? altExePath : exePaths[index];
        if (!string.IsNullOrEmpty(targetPath))
        {
            if (targetPath.All(char.IsDigit))
            {
                string steamLaunchCommand = $"steam://rungameid/{targetPath}";
                UnityEngine.Debug.Log($"Launching Steam game with ID {targetPath}...");
                Process.Start(new ProcessStartInfo(steamLaunchCommand) { UseShellExecute = true });
            }
            else
            {
                UnityEngine.Debug.Log($"Opening Steam Store page: {targetPath}");
                Process.Start(new ProcessStartInfo(targetPath) { UseShellExecute = true });
            }
            yield return new WaitForSeconds(1);
            if (quitOnLaunch[index])
            {
                UnityEngine.Debug.Log("Closing Unity after launching Steam game/store link...");
                Application.Quit();
            }
        }
        else
        {
            UnityEngine.Debug.LogError($"Entry at index {index} is empty!");
        }
    }
}