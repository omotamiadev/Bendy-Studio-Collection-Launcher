using System.Diagnostics;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ExeLauncher : MonoBehaviour
{
    public Button[] launchButtons;
    public string[] exePaths = new string[10];
    public bool[] quitOnLaunch = new bool[10];

    public Button altLaunchButton1;
    public Button altLaunchButton2;
    public string altExePath1;
    public string altExePath2;

    private int altVersionIndex = 0;
    private bool altModeActive = false;
    private const int AltIndex = 3;
    void Start()
    {
        for (int i = 0; i < launchButtons.Length; i++)
        {
            int buttonIndex = i;
            launchButtons[i].onClick.AddListener(() => StartCoroutine(OpenExeOrStore(buttonIndex)));
        }
        altLaunchButton1.onClick.AddListener(() => HandleAltClick(1));
        altLaunchButton2.onClick.AddListener(() => HandleAltClick(2));
        altLaunchButton1.gameObject.SetActive(false);
        altLaunchButton2.gameObject.SetActive(false);
    }
    public void ToggleAltVersion()
    {
        altModeActive = !altModeActive;
        launchButtons[AltIndex].gameObject.SetActive(!altModeActive);
        altLaunchButton1.gameObject.SetActive(altModeActive);
        altLaunchButton2.gameObject.SetActive(altModeActive);
        altVersionIndex = altModeActive ? 1 : 0;
    }
    private void HandleAltClick(int version)
    {
        altVersionIndex = version;
        StartCoroutine(OpenExeOrStore(AltIndex));
    }
    IEnumerator OpenExeOrStore(int index)
    {
        if (index < 0 || index >= exePaths.Length)
        {
            yield break;
        }
        string targetPath = exePaths[index];
        bool shouldQuit = quitOnLaunch[index];
        if (index == AltIndex && altModeActive)
        {
            targetPath = altVersionIndex == 1 ? altExePath1 : altExePath2;
            shouldQuit = false;
        }
        if (!string.IsNullOrEmpty(targetPath))
        {
            if (targetPath.All(char.IsDigit))
            {
                string steamUri = $"steam://rungameid/{targetPath}";
                Process.Start(new ProcessStartInfo(steamUri) { UseShellExecute = true });
            }
            else
            {
                Process.Start(new ProcessStartInfo(targetPath) { UseShellExecute = true });
            }
            yield return new WaitForSeconds(1);
            if (shouldQuit)
            {
                Application.Quit();
            }
        }
    }
    private const string secretsOfTheMachineAppID = "2862330";
    private const string bendyAndTheInkMachineAppID = "622650";
    private const string bendyAndTheDarkRevivalAppID = "1063660";
    private const string bendyTheCageAppID = "2663960";
    private const string bendyAndTheDarkSurvivalAppID = "1236990";
    public void LaunchSecretsOfTheMachine() => LaunchSteamGame(secretsOfTheMachineAppID, 1);
    public void LaunchBendyAndTheInkMachine() => LaunchSteamGame(bendyAndTheInkMachineAppID, 2);
    public void LaunchBendyAndTheDarkRevival() => LaunchSteamGame(bendyAndTheDarkRevivalAppID, 4);
    public void LaunchBorisAndTheDarkSurvival() => LaunchSteamGame(bendyAndTheDarkSurvivalAppID, 5);
    private void LaunchSteamGame(string appID, int index)
    {
        string steamUri = $"steam://run/{appID}";
        {
            Process.Start(new ProcessStartInfo(steamUri) { UseShellExecute = true });
            if (index >= 0 && index < quitOnLaunch.Length && quitOnLaunch[index])
            {
                Application.Quit();
            }
        }
    }
    public void LaunchBendyTheCage(int index)
    {
        string steamUri = $"steam://run/2663960";
        {
            Process.Start(new ProcessStartInfo(steamUri) { UseShellExecute = true });
        }
    }
}