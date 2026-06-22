using UnityEngine;

public class GameplayManager : SceneManagerBase<GameplayManager>
{
    private ResultData resultData = new ResultData();
    private PlayerController player;

    protected override void StateStart()
    {
        // プレイヤーの起動
        player = FindAnyObjectByType<PlayerController>();
        player.enabled = true;

        base.StateStart();
    }
    protected override void StateRunning()
    {
        // 現在のスコアを設定
        SetScore((int)player.transform.position.z);
        base.StateRunning();
    }

    /// <summary>
    /// スコア設定    </summary>
    public void SetScore(int score) { resultData.score = score; }
    /// <summary>
    /// スコア取得    </summary>
    public int GetScore() { return resultData.score; }
    /// <summary>
    /// ゲームオーバー処理    </summary>
    public void GameOver()
    {
        //TODO: 直近プレイのスコア保存
        Debug.Log("スコア:" + resultData.score);
        // リザルトへ遷移
        ChangeScene(SceneType.Result, false);
    }
}
