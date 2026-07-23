using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンの種類    </summary>
public enum SceneType
{
    None,
    /// <summary>
    /// 起動処理(初期設定・初期ロードなど)    </summary>
    Boot = 0,
    /// <summary>
    /// タイトル    </summary>
    Title = 1,
    /// <summary>
    ///     </summary>
    Select = 10,
    /// <summary>
    /// ゲームのプレイ部分    </summary>
    Gameplay = 20,
    /// <summary>
    /// リザルト    </summary>
    Result = 30,
    /// <summary>
    /// 終了    </summary>
    Quit = 100,
}
/// <summary>
/// シーンの状態    </summary>
public enum SceneState
{
    /// <summary>
    /// 特になし    </summary>
    None,
    /// <summary>
    /// 待機状態(シーンのロード待ちに使用)    </summary>
    Idle,
    /// <summary>
    /// 初期化    </summary>
    Init,
    /// <summary>
    /// 開始    </summary>
    Start,
    /// <summary>
    /// 実行中    </summary>
    Running,
    /// <summary>
    /// 終了    </summary>
    End,
    /// <summary>
    /// 停止    </summary>
    Uninit,
}

public abstract class SceneManagerBase : MonoBehaviour
{
    /// <summary>
    /// 現在アクティブなシーンマネージャー    </summary>
    public static SceneManagerBase activeManager;
    /// <summary>
    /// 現在のシーン状態    </summary>
    public static  SceneState currentState = SceneState.Init;
    /// <summary>
    /// 現在のシーン名    </summary>
    public static string sceneName;

    private static AsyncOperation asyncOperation;

    private SceneType tmpNextSceneType;
    private bool tmpIsTransition;
    private bool tmpIsLoadScene;
    private string tmpLoadSceneName;
    private bool isWaitSceneChange; // シーン遷移の待機状態かどうか

    private void OnEnable() { activeManager = this; }
    protected void Update()
    {
        // シーン状態ごとの専用処理
        switch (currentState)
        {
            case SceneState.Idle: StateIdle(); break;
            case SceneState.Init: StateInit(); break;
            case SceneState.Start: StateStart(); break;
            case SceneState.Running: StateRunning(); break;
            case SceneState.End: StateEnd(); break;
            case SceneState.Uninit: StateUninit(); break;
        }
    }

    /// <summary>
    /// シーンマネージャーの切り替え    </summary>
    /// <param name="nextSceneType">
    /// 切り替えるシーンマネージャーに対応したシーンの種類    </param>
    private void ChangeSceneManager(SceneType nextSceneType)
    {
        // 現在のシーンマネージャーの初期化
        enabled = false;
        currentState = SceneState.Idle;
        // 切り替え
        switch (nextSceneType)
        {
            case SceneType.Title: GetComponent<SceneManagerBase<TitleManager>>().enabled = true; break;
            case SceneType.Select: GetComponent<SceneManagerBase<StageSelectManager>>().enabled = true; break;
            case SceneType.Gameplay: GetComponent<SceneManagerBase<GameplayManager>>().enabled = true; break;
            case SceneType.Result: GetComponent<SceneManagerBase<ResultManager>>().enabled = true; break;
        }
    }
    /// <summary>
    /// シーンをロード    </summary>
    /// <param name="loadSceneName">
    /// ロードするシーン名    </param>
    private void LoadScene(string loadSceneName)
    {
        sceneName = loadSceneName;
        asyncOperation = SceneManager.LoadSceneAsync(loadSceneName);
        ChangeSceneManager(tmpNextSceneType);
    }
    /// <summary>
    /// シーン名を取得    </summary>
    /// <param name="sceneType">
    /// 取得するシーンの種類    </param>
    private string GetSceneName(SceneType sceneType)
    {
        //** シーン遷移呼び出し時にシーン名が「渡されている」
        // 渡されているシーン名を返す
        if (!string.IsNullOrEmpty(tmpLoadSceneName)) return tmpLoadSceneName;
        //** シーン遷移呼び出し時にシーン名が「渡されていない」
        // シーンの種類名 + "Scene"で返す
        // (例) SceneType.Title → TitleScene
        Debug.Log(
            "遷移先シーン名が設定されていないため、デフォルト値を返しました。\n" +
            $"シーン名: {sceneType.ToString()}Scene");
        return sceneType.ToString() + "Scene";
    }
    /// <summary>
    /// アプリケーション終了    </summary>
    private void QuitApplication()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        // ゲームを終了
        Application.Quit();
#endif
    }

    protected virtual void StateIdle()
    {
        if (asyncOperation == null || asyncOperation.isDone)
            currentState++;
    }
    protected virtual void StateInit() { currentState++; }
    protected virtual void StateStart() { currentState++; }
    protected virtual void StateRunning() { if(isWaitSceneChange) currentState++; }
    protected virtual void StateEnd() { currentState++; }
    protected virtual void StateUninit()
    {
        // シーン遷移待ち状態であれば、シーン遷移
        if (isWaitSceneChange)
            HandleSceneChange(tmpNextSceneType, tmpIsTransition, tmpIsLoadScene, tmpLoadSceneName);
    }
    private void HandleSceneChange(SceneType nextSceneType, bool isTransition, bool isLoadScene, string loadSceneName = "")
    {
        // 後処理状態ならシーン遷移実行
        if (currentState == SceneState.Uninit)
        {
            isWaitSceneChange = false;
            if (nextSceneType == SceneType.Quit) QuitApplication();  //ゲーム終了
            else
            {
                // シーンのロード有無で処理分岐
                if (isLoadScene)
                {
                    string sceneName = GetSceneName(nextSceneType);
                    // 遷移アニメーションの有無で処理分岐
                    if (isTransition) FadeManager.Instance.FadeOut(-1, () => LoadScene(sceneName), true);
                    else LoadScene(sceneName);
                }
                else ChangeSceneManager(nextSceneType);  // マネージャーだけ切り替え
            }
        }
        else isWaitSceneChange = true;  // 後処理状態まで待機
    }

    /// <summary>
    /// シーン遷移    </summary>
    /// <param name="nextSceneType">
    /// 遷移シーンタイプ    </param>
    /// <param name="isTransition">
    /// 遷移アニメーションの有無    </param>
    /// <param name="loadSceneName">
    /// ロードするシーン名(入力が無ければ、シーンの種類名 + Scene)    </param>
    public void ChangeScene(SceneType nextSceneType, bool isTransition = true, string loadSceneName = "")
    {
        // 値の仮保存
        tmpNextSceneType = nextSceneType;
        tmpIsTransition = isTransition;
        tmpIsLoadScene = true;
        tmpLoadSceneName = loadSceneName;
        // 本処理
        HandleSceneChange(tmpNextSceneType, tmpIsTransition, tmpIsLoadScene, tmpLoadSceneName);
    }
    /// <summary>
    /// 同シーンでのシーン遷移    </summary>
    /// <param name="nextSceneType">
    /// 遷移シーンタイプ    </param>
    /// <param name="isTransition">
    /// 遷移アニメーションの有無    </param>
    public void ChangeSceneWithoutLoad(SceneType nextSceneType, bool isTransition = false)
    {
        // 値の仮保存
        tmpNextSceneType = nextSceneType;
        tmpIsTransition = isTransition;
        tmpIsLoadScene = false;
        tmpLoadSceneName = "";
        // 本処理
        HandleSceneChange(tmpNextSceneType, tmpIsTransition, tmpIsLoadScene, tmpLoadSceneName);
    }
}
/// <summary>
/// 継承時の記述方式 TestSceneManager : SceneManagerBase<TestSceneManager>    </summary>
public abstract class SceneManagerBase<T> : SceneManagerBase where T : SceneManagerBase<T>
{
    /// <summary>
    /// インスタンス    </summary>
    private static T instance;
    public static T Instance => instance;

    protected void Awake()
    {
        // インスタンス化
        if (instance == null)
        {
            instance = this as T;
            sceneName = SceneManager.GetActiveScene().name;
        }
    }
}