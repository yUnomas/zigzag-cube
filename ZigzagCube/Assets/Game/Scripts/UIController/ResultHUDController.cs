using System;
using TMPro;
using UnityEngine;

public class ResultHUDController : UIControllerBase
{
    [SerializeField] TextMeshProUGUI scoreTMP;
    [SerializeField] TextMeshProUGUI highScoreTMP;
    [SerializeField] TextMeshProUGUI newRecordTMP;
    [SerializeField] TextMeshProUGUI playTimeTMP;
    
    /// <summary>
    /// プレイ時間の表示更新    </summary>
    private void UpdatePlayTime(float playTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(playTime);
        // 1時間を超えていたら、「時:分:秒」で表示
        if (timeSpan.TotalHours >= 1)
        {
            playTimeTMP.text = $"Play Time: {(int)timeSpan.TotalHours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
        // 「分:秒」で表示
        else
        {
            playTimeTMP.text = $"Play Time: {timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
    }

    /// <summary>
    /// リザルト表示    </summary>
    public void ShowResult(ResultData resultData)
    {
        // 数値の表示更新
        scoreTMP.text = resultData.score.ToString();
        highScoreTMP.text = $"High Score: {resultData.highScore}";
        UpdatePlayTime(resultData.playTime);
        // ハイスコアの更新状況によって、新記録ラベルを表示
        if (resultData.isUpdatedHighScore)
        {
            newRecordTMP.enabled = true;
        }
    }
    /// <summary>
    /// （タイトルへ）戻るボタンの押下イベント    </summary>
    public void OnReturnButtonPressed()
    {
        ResultManager.Instance.BackToTile();
    }
}
