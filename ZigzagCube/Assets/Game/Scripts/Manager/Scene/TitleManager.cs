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
    protected override void StateStart()
    {
        AudioManager.Instance.PlayBGM("TitleMain");
        base.StateStart();
    }

    /// <summary>
    /// ゲーム開始    </summary>
    public void StartGame()
    {
        ChangeScene(SceneType.Gameplay, false);
    }
}
