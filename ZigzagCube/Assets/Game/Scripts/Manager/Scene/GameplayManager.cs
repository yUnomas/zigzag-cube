using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameplayManager : SceneManagerBase<GameplayManager>
{
    private int score;
    public int Score => score;

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
        // 現在のスコアを設定 / 表示更新
        SetScore((int)player.transform.position.z);
        base.StateRunning();
    }
    protected override void StateEnd()
    {
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
        };
        ResultManager.Instance.SetResult(resultData);

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
    }
}
