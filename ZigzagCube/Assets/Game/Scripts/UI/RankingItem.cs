using TMPro;
using UnityEngine;

public class RankingItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rankTMP;
    [SerializeField] TextMeshProUGUI playerNameTMP;
    [SerializeField] TextMeshProUGUI scoreTMP;

    /// <summary>
    /// ランキングの表示設定    </summary>
    public void SetRanking(int rank, ScoreRecord scoreRecord)
    {
        rankTMP.SetText(rank.ToString());
        playerNameTMP.SetText(scoreRecord.playerName);
        scoreTMP.SetText(scoreRecord.score.ToString());
    }
    /// <summary>
    /// ランキングのデフォルト表示設定    </summary>
    public void SetDefault(int rank)
    {
        rankTMP.SetText(rank.ToString());
        playerNameTMP.SetText("-----");
        scoreTMP.SetText("---");
    }
}