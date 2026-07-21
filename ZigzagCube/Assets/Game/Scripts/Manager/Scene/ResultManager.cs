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
        if (AdsManager.Instance.IsInterstitialTiming()) Debug.Log("インタースティシャル広告の表示を行うため、遷移のみ");
        else Debug.Log("インタースティシャル広告の表示を行わないため、遷移アニメを実行");

        ChangeScene(SceneType.Title, true, "GameplayScene");
    }
}
