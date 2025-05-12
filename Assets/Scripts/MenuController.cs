using UnityEngine;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    public Button SettingsButton; 
    public GameObject SettingsPanel; 
    void Start()
    {
        if (SettingsButton != null && SettingsPanel != null)
        {
            SettingsButton.onClick.AddListener(ToggleSettings);
        }
    }
    void ToggleSettings()
    {
        if (SettingsPanel != null)
        {
            SettingsPanel.SetActive(!SettingsPanel.activeSelf); 
        }
    }
    public void ExitLauncher()
    {
        Application.Quit();
        Debug.Log("Quitting Game...");
    }
}