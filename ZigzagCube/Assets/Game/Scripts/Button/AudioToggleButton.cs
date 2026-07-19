using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioToggleButton : UIButtonController
{
    [SerializeField, Tooltip("音声タイプ")]
    private AudioType type;
    [SerializeField] private TextMeshProUGUI buttonIdleLabel;
    [SerializeField] private TextMeshProUGUI buttonPressedLabel;
    
    /// <summary>
    /// ミュート状態    </summary>
    private bool isMute;
    private AudioManager audioManager;
    private SaveDataManager saveDataManager;

    private void Start()
    {
        // インスタンス取得
        audioManager = AudioManager.Instance;
        saveDataManager = SaveDataManager.Instance;
        // ボタンの音声タイプによる初期化
        switch(type)
        {
            case AudioType.BGM: Initialize(saveDataManager.SettingsData.bgmVolume); break;
            case AudioType.SE: Initialize(saveDataManager.SettingsData.seVolume); break;
            case AudioType.SFX: Initialize(saveDataManager.SettingsData.sfxVolume); break;
        }
    }

    private void Initialize(float volume)
    {
        isMute = volume < 0.1f;
        ToggleButtonVisual();
    }
    /// <summary>
    /// 音量切り替え    </summary>
    private void ToggleAudioVolume(AudioType type, float volume)
    {
        // 音量の調整
        audioManager.SetVolume(type, volume);
        // 設定の保存
        SettingsData data = saveDataManager.SettingsData;
        switch (type)
        {
            case AudioType.BGM: data.bgmVolume = volume; break;
            case AudioType.SE: data.seVolume = volume;  break;
            case AudioType.SFX: data.sfxVolume = volume; break;
        }
        saveDataManager.Save<SettingsData>(data);
    }
    /// <summary>
    /// ボタンの表示切り替え    </summary>
    private void ToggleButtonVisual()
    {
        buttonIdleLabel.text = isMute ? "OFF" : "ON";
        buttonPressedLabel.text = isMute ? "OFF" : "ON";
        buttonIdleLabel.color = isMute ? Color.red : Color.white;
        buttonPressedLabel.color = isMute ? Color.red / 2 : Color.white / 2;
    }

    public void OnClick()
    {
        isMute = !isMute;   // ミュート状態の切り替え
        ToggleAudioVolume(type, isMute ? 0f : 1f);
        ToggleButtonVisual();
    }
}
