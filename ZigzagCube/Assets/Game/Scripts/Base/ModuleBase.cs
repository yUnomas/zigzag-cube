using UnityEngine;

/// <summary>
/// 専用コントローラーを使用する場合は、型指定    </summary>
public abstract class ModuleBase : MonoBehaviour
{
    protected ControllerBase baseController;
    public virtual void SetController(ControllerBase controller)
    {
        baseController = controller;
    }

    /// <summary>
    /// 初期化処理    </summary>
    public virtual void Initialize(){}
    /// <summary>
    /// 動作開始    </summary>
    public virtual void Activate(){}
    /// <summary>
    /// 動作終了    </summary>
    public virtual void Deactivate(){}
    /// <summary>
    /// 更新処理    </summary>
    public virtual void Execute(InputData inputData){}
    /// <summary>
    /// 物理更新処理    </summary>
    public virtual void FixedExecute(){}
}
public abstract class ModuleBase<T> : ModuleBase where T : ControllerBase
{
    /// <summary>
    /// 専用コントローラー    </summary>
    protected T controller { get; private set; }
    public override void SetController(ControllerBase controller)
    {
        base.SetController(controller);
        // 型チェック
        if (controller is not T typedController)
        {
            Debug.LogError(
                $"{GetType().Name}には{typeof(T).Name}が必要ですが、" +
                $"{controller.GetType().Name}が渡されました",
                this
            );
            return;
        }
        // 型が合えばキャスト後の値を保持
        this.controller = typedController;
    }
}
