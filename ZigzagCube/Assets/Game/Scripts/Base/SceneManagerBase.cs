#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンの種類    </summary>
public enum SceneType
{
    /// <summary>
    /// 特になし    </summary>
    None = -1,
    /// <summary>
    /// 起動処理(初期設定・初期ロードなど)    </summary>
    Boot = 0,
    /// <summary>
    /// タイトル    </summary>
    Title = 1,
    /// <summary>
    /// ステージ選択    </summary>
    StageSelect = 10,
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
    /// シーンのロード待ち    </summary>
    Idle,
    /// <summary>
    /// 初期化    </summary>
    Init,
    /// <summary>
    /// 開始    </summary>
    Start,
    /// <summary>
    /// 毎フレーム実行    </summary>
    Update,
    /// <summary>
    /// 終了    </summary>
    End,
    /// <summary>
    /// 後処理    </summary>
    Uninit,
}

public abstract class SceneManagerBase : MonoBehaviour
{
    private struct SceneChangeRequest
    {
        public SceneType sceneType;
        public string sceneName;
        public bool isUseTransition;
        public bool isLoadScene;
    }

    public static SceneManagerBase activeManager { get; private set; }
    public static  SceneState currentState = SceneState.Init;
    public static string sceneName;

    private static AsyncOperation asyncOperation;
    private SceneChangeRequest sceneChangeRequest;

    /// <summary>
    /// シーン遷移の待機状態   </summary>
    private bool isWaitingSceneChange;

    private void OnEnable() { activeManager = this; }
    private void Update()
    {
        // シーン状態ごとの専用処理
        switch (currentState)
        {
            case SceneState.Idle:
                OnIdle();
                if (asyncOperation != null && !asyncOperation.isDone) return;
                FadeManager.Instance.TryFadeIn();
                currentState = SceneState.Init;
                break;
            case SceneState.Init:
                OnInit();
                currentState = SceneState.Start;
                break;
            case SceneState.Start:
                OnStart();
                currentState = SceneState.Update;
                break;
            case SceneState.Update:
                OnUpdate();
                if (isWaitingSceneChange) currentState = SceneState.End;
                break;
            case SceneState.End:
                OnEnd();
                currentState = SceneState.Uninit;
                break;
            case SceneState.Uninit:
                OnUninit();
                // シーン遷移待ち状態であれば、シーン遷移
                if (isWaitingSceneChange)
                {
                    HandleSceneChange(sceneChangeRequest);
                }
                else currentState = SceneState.Idle;
                break;
        }
    }

    /// <summary>
    /// シーンマネージャーの切り替え    </summary>
    /// <param name="nextSceneType">
    /// 切り替えるシーンマネージャーに対応したシーンの種類    </param>
    private void ChangeSceneManager(SceneType nextSceneType)
    {
        enabled = false;    // 現在のシーンマネージャーの初期化
        switch (nextSceneType)
        {
            case SceneType.Title: GetComponent<SceneManagerBase<TitleManager>>().enabled = true; break;
            case SceneType.StageSelect: GetComponent<SceneManagerBase<StageSelectManager>>().enabled = true; break;
            case SceneType.Gameplay: GetComponent<SceneManagerBase<GameplayManager>>().enabled = true; break;
            case SceneType.Result: GetComponent<SceneManagerBase<ResultManager>>().enabled = true; break;
            default: Debug.LogError($"対応していないSceneTypeです: {nextSceneType}"); return;
        }
        currentState = SceneState.Idle; // 切り替え成功時に状態遷移
    }
    /// <summary>
    /// シーンをロード    </summary>
    private void LoadScene(SceneType loadSceneType, string loadSceneName)
    {
        sceneName = loadSceneName;
        asyncOperation = SceneManager.LoadSceneAsync(loadSceneName);
        ChangeSceneManager(loadSceneType);
    }
    /// <summary>
    /// シーン名を取得    </summary>
    /// <param name="sceneType">
    /// 取得するシーンの種類    </param>
    /// <param name="specifiedSceneName">
    /// 指定されたシーン名</param>
    private string GetSceneName(SceneType sceneType, string specifiedSceneName)
    {
        // シーン遷移呼び出し時にシーン名が指定されていれば、そのまま返す
        if (!string.IsNullOrEmpty(specifiedSceneName)) return specifiedSceneName;
        // 指定されていなければ、デフォルト値(シーンの種類名 + "Scene")を返す
        string defaultSceneName = $"{sceneType}Scene";
        Debug.Log(
            "遷移先シーン名が設定されていないため、デフォルト値を使用\n" +
            $"シーン名: {defaultSceneName}");
        return defaultSceneName;
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

    protected virtual void OnIdle(){}
    protected virtual void OnInit(){}
    protected virtual void OnStart(){}
    protected virtual void OnUpdate(){}
    protected virtual void OnEnd(){}
    protected virtual void OnUninit(){}
    private void HandleSceneChange(SceneChangeRequest request)
    {
        isWaitingSceneChange = false;
        if (request.sceneType == SceneType.Quit) QuitApplication();  //ゲーム終了
        else
        {
            // シーンのロード有無で処理分岐
            if (request.isLoadScene)
            {
                string sceneName = GetSceneName(request.sceneType, request.sceneName);
                // 遷移アニメーションの有無で処理分岐
                if (request.isUseTransition)
                {
                    FadeManager.Instance.FadeOut(-1, () => LoadScene(request.sceneType, sceneName));
                }
                else LoadScene(request.sceneType, sceneName);
            }
            else
            {
                // 遷移アニメーションの有無で処理分岐
                if (request.isUseTransition)
                {
                    FadeManager.Instance.FadeOut(-1, () => ChangeSceneManager(request.sceneType));
                }
                else ChangeSceneManager(request.sceneType);
            }
        }
    }

    /// <summary>
    /// シーン遷移    </summary>
    /// <param name="nextSceneType">
    /// 遷移シーンタイプ    </param>
    /// <param name="isUseTransition">
    /// 遷移アニメーションの有無    </param>
    /// <param name="loadSceneName">
    /// ロードするシーン名(入力が無ければ、シーンの種類名 + Scene)    </param>
    public void ChangeScene(SceneType sceneType, bool isUseTransition = true, string sceneName = "")
    {
        // シーン遷移のリクエスト作成
        sceneChangeRequest = new SceneChangeRequest()
        {
            sceneType = sceneType,
            sceneName = sceneName,
            isUseTransition = isUseTransition,
            isLoadScene = true
        };
        // 後処理状態まで待機
        isWaitingSceneChange = true;
    }
    /// <summary>
    /// 同シーンでのシーン遷移    </summary>
    /// <param name="nextSceneType">
    /// 遷移シーンタイプ    </param>
    /// <param name="isTransition">
    /// 遷移アニメーションの有無    </param>
    public void ChangeSceneWithoutLoad(SceneType sceneType, bool isUseTransition = false)
    {
        // シーン遷移のリクエスト作成
        sceneChangeRequest = new SceneChangeRequest()
        {
                sceneType = sceneType,
                sceneName = "",
                isUseTransition = isUseTransition,
                isLoadScene = false
        };
        // 後処理状態まで待機
        isWaitingSceneChange = true;
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