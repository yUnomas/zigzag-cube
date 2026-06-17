using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerBase : MonoBehaviour
{
    [SerializeField, Tooltip("挙動リスト")]
    protected List<BehaviorBase> behaviors = new List<BehaviorBase>();

    protected virtual void Start()
    {
        // 初期化
        foreach (var behavior in behaviors)
        {
            behavior.Initialize();
        }
    }
    protected virtual void Update()
    {
        InputData inputData = CreateInputData();
        // 各挙動の実行
        foreach (var behavior in behaviors)
        {
            behavior.Execute(inputData);
        }
    }

    /// <summary>
    /// 入力情報の作成    </summary>
    protected virtual InputData CreateInputData()
    {
        InputData data = new InputData();
        return data;
    }
}
