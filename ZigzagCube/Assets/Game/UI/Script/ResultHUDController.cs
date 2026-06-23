using UnityEngine;

public class ResultHUDController : UIControllerBase
{
    /// <summary>
    /// ゲームの再挑戦ボタンの押下イベント    </summary>
    public void OnRetryButtonPressed()
    {
        ResultManager.Instance.RetryGame();
    }
    /// <summary>
    /// タイトルへ戻るボタンの押下イベント    </summary>
    public void OnBackToTitleButtonPressed()
    {
        ResultManager.Instance.BackToTile();
    }
}
