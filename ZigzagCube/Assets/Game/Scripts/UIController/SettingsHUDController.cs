using UnityEngine;

public class SettingsHUDController : UIControllerBase
{
    private GameObject settingsButton;

    /// <summary>
    /// 設定画面を開くイベント    </summary>
    public void OnSettingsOpened(SettingsButton button)
    {
        Show();
        // 設定ボタンを非表示
        settingsButton = button.gameObject;
        settingsButton.SetActive(false);
    }
    /// <summary>
    /// 設定画面を閉じるイベント    </summary>
    public void OnSettingsClosed()
    {
        Hide();
        // 設定ボタンを表示
        settingsButton.SetActive(true);
    }
}
