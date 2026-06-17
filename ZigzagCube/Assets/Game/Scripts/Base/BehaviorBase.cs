using UnityEngine;

public abstract class BehaviorBase : MonoBehaviour
{
    /// <summary>
    /// 初期化処理    </summary>
    public virtual void Initialize(){}
    /// <summary>
    /// 実処理    </summary>
    public abstract void Execute(InputData inputData);
}
