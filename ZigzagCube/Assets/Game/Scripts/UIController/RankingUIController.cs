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
            rankingItems[i].UpdateRank(i + 1);  // 順位更新

            // スコア履歴が記録されていれば、ランキングとしてスコア表示
            if (i < records.Count)
            {
                rankingItems[i].SetRanking(records[i]);
            }
            // スコア履歴がないため、ランキング表示を初期化
            else
            {
                rankingItems[i].Clear();
            }
        }
    }
}