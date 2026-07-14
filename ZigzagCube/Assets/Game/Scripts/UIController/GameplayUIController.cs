using TMPro;
using UnityEngine;

public class GameplayUIController : UIControllerBase
{
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private PauseUIController pauseUI;

    /// <summary>
    /// スコアの表示更新    </summary>
    /// <param name="score">
    /// 現在のスコア  </param>
    public void UpdateScoreText(int score)
    {
        scoreTMP.text = $"{score}";
    }

    /// <summary>
    /// ポーズボタンが押された際のイベント    </summary>
    public void OnClickPause()
    {
        GameplayManager.Instance.TogglePause();
        pauseUI.Show();
    }
    /// <summary>
    ///ポーズパネルが押された際のイベント     </summary>
    public void OnClickPausePanel()
    {
        GameplayManager.Instance.TogglePause();
        pauseUI.Hide();
    }
}
