using UnityEngine;

public class TitleManager : SceneManagerBase<TitleManager>
{
    private TitleUIController titleUI;

    protected override void StateInit()
    {
        titleUI = FindAnyObjectByType<TitleUIController>();
        titleUI.Show();
        base.StateInit();
    }
    /// <summary>
    /// ゲーム開始    </summary>
    public void StartGame()
    {
        ChangeScene(SceneType.Gameplay, false);
    }
}
