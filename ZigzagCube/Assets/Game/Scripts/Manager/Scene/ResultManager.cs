using UnityEngine;

public class ResultManager : SceneManagerBase<ResultManager>
{
    private ResultData resultData;
    private ResultUIController resultUI;

    protected override void StateInit()
    {
        resultUI = FindAnyObjectByType<ResultUIController>();
        resultUI.Show();
        AudioManager.Instance.PlaySE("ResultClear");
        base.StateInit();
    }
    protected override void StateStart()
    {
        resultUI.ShowResult(resultData);
        base.StateStart();
    }

    /// <summary>
    /// リザルト情報を設定    </summary>
    public void SetResult(ResultData result) { resultData = result; }
    /// <summary>
    /// タイトルに戻る    </summary>
    public void BackToTile()
    {
        // 広告の表示有無でシーン遷移のアニメーション切り替え
        if (AdsManager.Instance.IsInterstitialTiming())
        {
            ChangeScene(SceneType.Title, false, "GameplayScene");
        }
        else ChangeScene(SceneType.Title, true, "GameplayScene");
    }
}
