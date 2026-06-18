using UnityEngine;

public class ResultManager : SceneManagerBase<ResultManager>
{
    protected override void StateRunning()
    {
        ChangeScene(SceneType.Gameplay, true);
        base.StateRunning();
    }
}
