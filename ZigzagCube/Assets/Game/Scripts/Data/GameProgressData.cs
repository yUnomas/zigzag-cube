using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム進行データ    </summary>
public class GameProgressData
{
    /// <summary>
    /// 最高スコア    </summary>
    public int highScore;
    /// <summary>
    /// スコア履歴    </summary>
    public List<ScoreRecord> scoreRecords = new();

    /// <summary>
    /// スコア履歴の追加    </summary>
    public void AddScoreRecord(int score, string playerName, int maxRecordCount)
    {
        // 今回のスコアを記録
        scoreRecords.Add(new ScoreRecord(score, playerName));
        // スコア履歴を降順ソート
        scoreRecords.Sort((a, b) => b.score.CompareTo(a.score));
        // 最大数を超えたスコア履歴を除外
        if (scoreRecords.Count > maxRecordCount)
        {
            scoreRecords.RemoveRange(maxRecordCount, scoreRecords.Count - maxRecordCount);
        }
    }
}
