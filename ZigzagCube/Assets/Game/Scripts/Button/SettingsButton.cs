using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    private SettingsHUDRoot settingsHUD;

    private void Awake()
    {
        settingsHUD = FindAnyObjectByType<SettingsHUDRoot>();
    }
    public void OnSettingsButtonPressed()
    {
        settingsHUD.OnSettingsOpened(this);
    }
}
