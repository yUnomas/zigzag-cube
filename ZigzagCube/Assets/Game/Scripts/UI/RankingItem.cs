using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingItem : MonoBehaviour
{
    [SerializeField] Image podiumCubeIcon;
    [SerializeField] Image backGroundImage;
    [SerializeField] TextMeshProUGUI rankTMP;
    [SerializeField] TextMeshProUGUI playerNameTMP;
    [SerializeField] TextMeshProUGUI scoreTMP;

    /// <summary>
    /// 順位の更新   </summary>
    public void UpdateRank(int rank)
    {
        // 通常順位の設定
        rankTMP.SetText(rank.ToString());
        if (rank > 3) return;   // 3位以降は早期リターン

        //表彰位置の順位設定
        rankTMP.color = new Color32(32, 32, 32, 255);
        backGroundImage.color = new Color32(96, 96, 96, 255);
        podiumCubeIcon.enabled = true;
        switch (rank)
        {
            case 1:
                {
                    podiumCubeIcon.color = new Color32(255, 215, 0, 255);
                    playerNameTMP.color = new Color32(255, 215, 0, 255);
                    scoreTMP.color = new Color32(255, 215, 0, 255);
                }
                break;
            case 2:
                {
                    podiumCubeIcon.color = new Color32(192, 192, 192, 255);
                    playerNameTMP.color = new Color32(192, 192, 192, 255);
                    scoreTMP.color = new Color32(192, 192, 192, 255);
                }
                break;
            case 3:
                {
                    podiumCubeIcon.color = new Color32(196, 112, 34, 255);
                    playerNameTMP.color = new Color32(196, 112, 34, 255);
                    scoreTMP.color = new Color32(196, 112, 34, 255);
                }
                break;
        }
    }
    /// <summary>
    /// ランキングの表示設定    </summary>
    public void SetRanking(ScoreRecord scoreRecord)
    {
        playerNameTMP.SetText(scoreRecord.playerName);
        scoreTMP.SetText(scoreRecord.score.ToString());
    }
    /// <summary>
    /// ランキングの初期化    </summary>
    public void Clear()
    {
        playerNameTMP.SetText("-----");
        scoreTMP.SetText("---");
    }
}