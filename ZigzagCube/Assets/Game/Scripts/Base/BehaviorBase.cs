using UnityEngine;

public abstract class BehaviorBase : MonoBehaviour
{
    /// <summary>
    /// 状態可否    </summary>
    protected bool isActive;

    /// <summary>
    /// 有効化    </summary>
    public virtual void Activate(){ isActive = true; }
    /// <summary>
    /// 無効化    </summary>
    public virtual void Deactivate(){ isActive = false; }
    /// <summary>
    /// 初期化処理    </summary>
    public virtual void Initialize(){}
    /// <summary>
    /// 実処理    </summary>
    public abstract void Execute(InputData inputData);
}
