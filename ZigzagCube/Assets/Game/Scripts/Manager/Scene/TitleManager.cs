using UnityEngine;

public class TitleManager : SceneManagerBase<TitleManager>
{
    private TitleUIController titleUI;

    protected override void StateInit()
    {
        // タイトルUI表示
        titleUI = FindAnyObjectByType<TitleUIController>();
        titleUI.Show();
        // インタースティシャル広告の表示を試みる
        AdsManager.Instance.TryShowAd(AdType.Interstitial);
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
        ChangeSceneWithoutLoad(SceneType.Gameplay);
    }
}
