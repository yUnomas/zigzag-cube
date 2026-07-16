using System;
using TMPro;
using UnityEngine;

public class ResultUIController : UIControllerBase
{
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private TextMeshProUGUI highScoreTMP;
    [SerializeField] private TextMeshProUGUI newRecordTMP;
    [SerializeField] private TextMeshProUGUI playTimeTMP;
    [SerializeField] private RankingUIController rankingUI;

    private ResultData resultData;

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
    public void ShowResult(ResultData result)
    {
        resultData = result;

        // 数値の表示更新
        scoreTMP.text = result.score.ToString();
        highScoreTMP.text = $"High Score: {result.highScore}";
        UpdatePlayTime(result.playTime);
        // ハイスコアの更新状況によって、新記録ラベルを表示
        if (result.isUpdatedHighScore)
        {
            newRecordTMP.enabled = true;
        }
    }
    /// <summary>
    /// （タイトルへ）戻るボタンが押された際のイベント    </summary>
    public void OnClickReturn()
    {
        ResultManager.Instance.BackToTile();
    }
    /// <summary>
    /// ランキング画面を開くボタンが押された際のイベント    </summary>
    public void OnClickOpenRanking()
    {
        Hide();
        rankingUI.Show();
    }
    /// <summary>
    /// 設定画面を閉じるボタンが押された際のイベント    </summary>
    public void OnClickCloseRanking()
    {
        rankingUI.Hide();
        Show();
    }
    /// <summary>
    /// 共有ボタンが押された際のイベント    </summary>
    public void OnClickShare()
    {
        GetComponent<SocialShareModule>().ShareResult(resultData);
    }
}
