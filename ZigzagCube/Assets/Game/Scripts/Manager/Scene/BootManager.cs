using UnityEngine;

public class BootManager : SceneManagerBase<BootManager>
{
    [SerializeField, Tooltip("デバッグ開始するシーンの種類")]
    private SceneType debugStartSceneType = SceneType.Boot;

    protected override void StateInit()
    {
        // 初期設定: 本来は設定ファイルのロード処理で行うべき
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        SaveDataManager.Instance.LoadAll();
        base.StateInit();
    }
    protected override void StateStart()
    {
#if UNITY_EDITOR
        // デバッグ開始
        if (debugStartSceneType != SceneType.Boot || debugStartSceneType != SceneType.None)
        {
            ChangeScene(debugStartSceneType, false);
            base.StateStart();
            return;
        }
#endif
        // ゲーム開始時に遷移するシーンを設定
        ChangeScene(SceneType.Title, true);
        base.StateStart();
    }
}