using UnityEngine;

public class ResultManager : SceneManagerBase<ResultManager>
{
    ResultHUDController resultHUD;

    protected override void StateInit()
    {
        resultHUD = FindAnyObjectByType<ResultHUDController>();
        resultHUD.Show();
        base.StateInit();
    }
    /// <summary>
    /// ゲームの再挑戦    </summary>
    public void RetryGame()
    {
        ChangeScene(SceneType.Gameplay, true, "GameplayScene");
    }
    /// <summary>
    /// タイトルに戻る    </summary>
    public void BackToTile()
    {
        ChangeScene(SceneType.Title, true, "GameplayScene");
    }
}
