using TMPro;
using UnityEngine;

public class GameplayHUDController : UIControllerBase
{
    [SerializeField] TextMeshProUGUI scoreTMP;

    /// <summary>
    /// スコアの表示更新    </summary>
    /// <param name="score">
    /// 現在のスコア  </param>
    public void UpdateScoreText(int score)
    {
        scoreTMP.text = $"{score}";
    }
}
