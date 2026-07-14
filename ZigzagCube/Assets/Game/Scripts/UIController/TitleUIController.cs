using UnityEngine;

public class TitleUIController : UIControllerBase
{
    [SerializeField] private SettingsUIController settingsUI;
    [SerializeField] private RankingUIController rankingUI;

    /// <summary>
    /// 開始ボタンが押された際のイベント    </summary>
    public void OnClickStart()
    {
        TitleManager.Instance.StartGame();
        Hide();
    }
    /// <summary>
    /// 設定画面を開くボタンが押された際のイベント    </summary>
    public void OnClickOpenSettings()
    {
        settingsUI.Show();
    }
    /// <summary>
    /// 設定画面を閉じるボタンが押された際のイベント    </summary>
    public void OnClickCloseSettings()
    {
        settingsUI.Hide();
    }
    /// <summary>
    /// ランキング画面を開くボタンが押された際のイベント    </summary>
    public void OnClickOpenRanking()
    {
        rankingUI.Show();
    }
    /// <summary>
    /// 設定画面を閉じるボタンが押された際のイベント    </summary>
    public void OnClickCloseRanking()
    {
        rankingUI.Hide();
    }
}
