using UnityEngine;

public class GameplayManager : SceneManagerBase<GameplayManager>
{
    private ResultData resultData = new ResultData();

    /// <summary>
    /// ゲームオーバー処理    </summary>
    /// <param name="playerPosition">
    /// プレイヤー座標 </param>
    public void GameOver(Vector3 playerPosition)
    {
        // 直近プレイのスコア保存
        resultData.score = (int)playerPosition.z;   // スコア(スタート位置からの移動距離)
        Debug.Log("スコア:" + resultData.score);

        // リザルトへ遷移
        ChangeScene(SceneType.Result, false);
    }
}
