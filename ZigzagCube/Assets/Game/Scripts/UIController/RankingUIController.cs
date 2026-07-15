using System.Collections.Generic;
using UnityEngine;

public class RankingUIController : UIControllerBase
{
    [SerializeField] private List<RankingItem> rankingItems;

    public override void Show()
    {
        base.Show();
        UpdateRanking(SaveDataManager.Instance.GameProgressData.scoreRecords);
    }

    /// <summary>
    /// ランキング更新    </summary>
    public void UpdateRanking(List<ScoreRecord> records)
    {
        // ランキング表示数のループ
        for (int i = 0; i < rankingItems.Count; i++)
        {
            // スコア履歴が記録されていれば、ランキングとしてスコア表示
            if (i < records.Count)
            {
                rankingItems[i].SetRanking(i + 1, records[i]);
            }
            // スコア履歴がないため、ランキング表示を初期化
            else
            {
                rankingItems[i].SetDefault(i + 1);
            }
        }
    }
}