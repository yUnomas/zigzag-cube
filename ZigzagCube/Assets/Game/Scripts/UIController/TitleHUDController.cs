using UnityEngine;

public class TitleHUDController : UIControllerBase
{
    /// <summary>
    /// ゲーム開始ボタンの押下イベント    </summary>
    public void OnStartGameButtonPressed()
    {
        TitleManager.Instance.StartGame();
        Hide();
    }
}
