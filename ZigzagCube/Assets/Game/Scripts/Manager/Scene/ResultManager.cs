using UnityEngine;

public class ResultManager : SceneManagerBase<ResultManager>
{
    private ResultData resultData;
    private ResultUIController resultUI;

    protected override void OnInit()
    {
        resultUI = FindAnyObjectByType<ResultUIController>();
        resultUI.Show();
        AudioManager.Instance.PlaySE("ResultClear");
    }
    protected override void OnStart()
    {
        resultUI.ShowResult(resultData);
    }

    /// <summary>
    /// リザルト情報を設定    </summary>
    public void SetResult(ResultData result) { resultData = result; }
    /// <summary>
    /// タイトルに戻る    </summary>
    public void BackToTile()
    {
        // 広告の表示有無でシーン遷移のアニメーション切り替え
        ChangeScene(SceneType.Title, true, "GameplayScene");
    }
}
