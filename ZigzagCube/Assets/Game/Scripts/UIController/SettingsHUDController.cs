using TMPro;
using UnityEngine;

public class SettingsHUDController : UIControllerBase
{
    [SerializeField] private PlayerNameField playerNameField;

    private SaveDataManager saveDataManager;
    private GameObject settingsButton;

    public override void Show()
    {
        saveDataManager = SaveDataManager.Instance;
        playerNameField.SetPlayerName(saveDataManager.PlayerData.name);
        base.Show();
    }

    /// <summary>
    /// プレイヤー名の更新    </summary>
    public void UpdatePlayerName(TMP_InputField inputField)
    {
        // 空欄の場合は元のプレイヤー名に戻す
        if (string.IsNullOrEmpty(inputField.text))
        {
            inputField.text = saveDataManager.PlayerData.name;
            return;
        }
        // 入力されたプレイヤー名に変更・保存
        saveDataManager.PlayerData.name = inputField.text;
        saveDataManager.Save(saveDataManager.PlayerData);
    }
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
