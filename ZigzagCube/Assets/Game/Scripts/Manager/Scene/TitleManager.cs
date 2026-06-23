using UnityEngine;

public class TitleManager : SceneManagerBase<TitleManager>
{
    private TitleHUDController titleHUD;

    protected override void StateInit()
    {
        titleHUD = FindAnyObjectByType<TitleHUDController>();
        titleHUD.Show();
        base.StateInit();
    }
    /// <summary>
    /// ゲーム開始    </summary>
    public void StartGame()
    {
        ChangeScene(SceneType.Gameplay, false);
    }
}
