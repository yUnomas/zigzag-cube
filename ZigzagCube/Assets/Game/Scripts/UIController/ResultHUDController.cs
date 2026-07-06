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
        // スコア表示
        scoreTMP.text = resultData.score.ToString();
        highScoreTMP.text = $"High Score: {resultData.highScore}";
        // ハイスコアの更新状況によって、新記録ラベルを表示
        if (resultData.isUpdatedHighScore)
        {
            newRecordTMP.enabled = true;
        }
    }
}
