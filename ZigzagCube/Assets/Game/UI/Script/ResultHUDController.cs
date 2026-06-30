using TMPro;
using UnityEngine;

public class ResultHUDController : UIControllerBase
{
    [SerializeField] TextMeshProUGUI scoreTMP;
    [SerializeField] TextMeshProUGUI highScoreTMP;
    [SerializeField] TextMeshProUGUI newRecordTMP;

    /// <summary>
    /// （タイトルへ）戻るボタンの押下イベント    </summary>
    public void OnReturnButtonPressed()
    {
        ResultManager.Instance.BackToTile();
    }

    /// <summary>
    /// リザルト表示    </summary>
    public void ShowResult(ResultData resultData)
    {
        // 今回のスコア
        scoreTMP.text = resultData.score.ToString();
        //** ハイスコアの更新状況によって、表示テキストを変更
        // 新記録ラベル
        if (resultData.isUpdatedHighScore)
        {
            newRecordTMP.enabled = true;
        }
        // 現在のハイスコア
        else
        {
            highScoreTMP.enabled = true;
            highScoreTMP.text = $"high score:{resultData.highScore}";
        }
    }
}
