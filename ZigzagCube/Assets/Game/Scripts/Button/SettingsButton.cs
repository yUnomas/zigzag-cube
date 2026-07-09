using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    private SettingsHUDController settingsHUD;

    private void Awake()
    {
        settingsHUD = FindAnyObjectByType<SettingsHUDController>();
    }
    public void OnSettingsButtonPressed()
    {
        settingsHUD.OnSettingsOpened(this);
    }
}
