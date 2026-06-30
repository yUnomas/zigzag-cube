using UnityEngine;

public class ResultManager : SceneManagerBase<ResultManager>
{
    private ResultData resultData;
    private ResultHUDController resultHUD;

    protected override void StateInit()
    {
        resultHUD = FindAnyObjectByType<ResultHUDController>();
        resultHUD.Show();
        base.StateInit();
    }
    protected override void StateStart()
    {
        resultHUD.ShowResult(resultData);
        base.StateStart();
    }

    /// <summary>
    /// リザルト情報を設定    </summary>
    public void SetResult(ResultData result) { resultData = result; }
    /// <summary>
    /// タイトルに戻る    </summary>
    public void BackToTile()
    {
        ChangeScene(SceneType.Title, true, "GameplayScene");
    }
}
