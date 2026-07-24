using UnityEngine;

public class GameplayManager : SceneManagerBase<GameplayManager>
{
    [SerializeField]
    private int maxRecordCount = 5;

    /// <summary>
    /// ゲーム内スコア    </summary>
    public int Score => score;
    private int score;
    /// <summary>
    /// プレイ時間    </summary>
    private float playTime;
    /// <summary>
    /// ポーズ状態    </summary>
    private bool isPaused;

    private PlayerController player;
    private GameplayUIController gameplayUI;
    private ResultManager resultManager;
    private SaveDataManager saveDataManager;

    private void Start()
    {
        resultManager = ResultManager.Instance;
        saveDataManager = SaveDataManager.Instance;
    }
    protected override void StateInit()
    {
        gameplayUI = FindAnyObjectByType<GameplayUIController>();
        player = FindAnyObjectByType<PlayerController>();
        base.StateInit();
    }
    protected override void StateStart()
    {
        Continue();
        gameplayUI.Show();
        AudioManager.Instance.PlayBGM("GameplayMain");
        base.StateStart();
    }
    protected override void StateRunning()
    {
        SetScore((int)player.transform.position.z); // スコア設定・表示更新
        playTime += Time.deltaTime;                 // プレイ時間の計測

        base.StateRunning();
    }
    protected override void StateEnd()
    {
        // HandleEndSateAsync()の処理待ち
    }
    protected override void StateUninit()
    {
        AudioManager.Instance.StopBGM();
        base.StateUninit();
    }

    private async Awaitable HandleEndStateAsync()
    {
        gameplayUI.Hide();
        //セーブデータの更新
        GameProgressData gameProgressData = saveDataManager.GameProgressData;
        gameProgressData.highScore = Mathf.Max(gameProgressData.highScore, score);
        gameProgressData.AddScoreRecord(
            this.score,
            saveDataManager.PlayerData.name,
            maxRecordCount);
        saveDataManager.Save(gameProgressData);
        // リザルト情報を作成しマネージャーに渡す
        ResultData resultData = new ResultData
        {
            score = this.score,
            highScore = gameProgressData.highScore,
            isUpdatedHighScore = this.score == gameProgressData.highScore,
            playTime = this.playTime,
        };
        resultManager.SetResult(resultData);
        // マネージャーのゲーム終了イベント発火
        AdsManager.Instance.OnGameplayEnded();
        // 1秒待機した後に状態遷移
        await Awaitable.WaitForSecondsAsync(1.0f);
        base.StateEnd();
    }
    /// <summary>
    /// スコア設定    </summary>
    private void SetScore(int value)
    {
        score = value;
        gameplayUI.UpdateScoreText(score); // 表示更新
    }
    /// <summary>
    /// ゲームを一時停止    </summary>
    private void Pause()
    {
        player.ChangeState(PlayerState.Idle);
    }
    /// <summary>
    /// ゲームを再開    </summary>
    private void Continue()
    {
        player.ChangeState(PlayerState.Alive);
    }

    /// <summary>
    /// ゲームプレイの終了処理    </summary>
    public void GameOver()
    {
        Debug.Log("スコア:" + score);
        // リザルトへ遷移
        ChangeSceneWithoutLoad(SceneType.Result);
        // 終了処理の発火
        _ = HandleEndStateAsync();
    }

    /// <summary>
    /// ポーズ状態の切り替え    </summary>
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused) Pause();
        else Continue();
    }
}
