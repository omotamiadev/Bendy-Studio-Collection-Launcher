using UnityEngine;
public class SwapAlt : MonoBehaviour
{
    public GameObject objectA;
    public GameObject objectB;
    private bool isAActive = true;
    private const string SaveKey = "SwapAlt_IsAActive";
    void Start()
    {
        isAActive = PlayerPrefs.GetInt(SaveKey, 1) == 1;
        ApplyState();
    }
    public void Toggle()
    {
        if (objectA == null || objectB == null)
            return;
        isAActive = !isAActive;
        PlayerPrefs.SetInt(SaveKey, isAActive ? 1 : 0);
        PlayerPrefs.Save();
        ApplyState();
    }
    private void ApplyState()
    {
        objectA.SetActive(isAActive);
        objectB.SetActive(!isAActive);
    }
}