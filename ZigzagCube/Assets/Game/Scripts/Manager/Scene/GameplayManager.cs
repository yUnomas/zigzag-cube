using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameplayManager : SceneManagerBase<GameplayManager>
{
    /// <summary>
    /// スコア    </summary>
    private int score;
    public int Score => score;
    /// <summary>
    /// プレイ時間    </summary>
    private float playTime;

    private PlayerController player;
    private GameplayHUDController gameplayHUD;

    protected override void StateInit()
    {
        gameplayHUD = FindAnyObjectByType<GameplayHUDController>();
        player = FindAnyObjectByType<PlayerController>();
        base.StateInit();
    }
    protected override void StateStart()
    {
        // プレイヤーの起動
        player.enabled = true;
        gameplayHUD.Show();
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
    private async Awaitable HandleEndStateAsync()
    {
        gameplayHUD.Hide();
        //セーブデータの更新
        GameRecordData gameRecordData = SaveDataManager.Instance.Load<GameRecordData>();
        gameRecordData.highScore = Mathf.Max(gameRecordData.highScore, score);
        SaveDataManager.Instance.Save(gameRecordData);
        // リザルト情報を作成しマネージャーに渡す
        ResultData resultData = new ResultData
        {
            score = this.score,
            highScore = gameRecordData.highScore,
            isUpdatedHighScore = this.score == gameRecordData.highScore,
            playTime = this.playTime,
        };
        ResultManager.Instance.SetResult(resultData);
        
        // 1秒待機した後に状態遷移
        await Awaitable.WaitForSecondsAsync(1.0f);
        base.StateEnd();
    }

    /// <summary>
    /// スコア設定    </summary>
    private void SetScore(int value)
    {
        score = value;
        gameplayHUD.UpdateScoreText(score); // 表示更新
    }
    /// <summary>
    /// ゲームプレイの終了処理    </summary>
    public void GameOver()
    {
        Debug.Log("スコア:" + score);
        // リザルトへ遷移
        ChangeScene(SceneType.Result, false);
        // 終了処理の発火
        _ = HandleEndStateAsync();
    }
}
