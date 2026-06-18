using UnityEngine;

public class GameplayManager : SceneManagerBase<GameplayManager>
{
    public void GameOver()
    {
        // 他のゲームオーバー処理があれば記述
        ChangeScene(SceneType.Result, false);
    }
}
