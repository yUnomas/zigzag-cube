using UnityEngine;

public class TitleManager : SceneManagerBase<TitleManager>
{
    /// <summary>
    /// ゲーム開始    </summary>
    public void StartGame()
    {
        ChangeScene(SceneType.Gameplay, false);
    }
}
