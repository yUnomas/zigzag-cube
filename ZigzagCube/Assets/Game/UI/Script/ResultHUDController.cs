using TMPro;
using UnityEngine;

public class ResultHUDController : UIControllerBase
{
    [SerializeField] TextMeshProUGUI scoreTMP;
    [SerializeField] TextMeshProUGUI highScoreTMP;

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

    /// <summary>
    /// リザルト表示    </summary>
    public void ShowResult(ResultData resultData)
    {
        scoreTMP.text = resultData.score.ToString();
        highScoreTMP.text = $"high score:{resultData.highScore}";
    }
}
