using UnityEngine;

public class GroundPoolController : MonoBehaviour
{
    [SerializeField] private StageObjectPool groundPool;
    [SerializeField] private StageObjectPool bridgePool;
    [SerializeField] private StageObjectPool conveyorPool;

    public StageObjectBase Get(GroundType type)
    {
        switch (type)
        {
            // 通常の地面
            case GroundType.Normal: return groundPool.Get();
            // 橋
            case GroundType.Bridge:
            case GroundType.MovingBridge:
            case GroundType.BridgeLane: return bridgePool.Get();
            // コンベヤー
            case GroundType.Conveyor: return conveyorPool.Get();

            default: return null;
        }
    }
    public void Release(GroundType type, StageObjectBase target)
    {
        if (type == GroundType.None || target == null) return;

        switch (type)
        {
            // 通常の地面
            case GroundType.Normal: groundPool.Release(target); break;
            // 橋
            case GroundType.Bridge:
            case GroundType.MovingBridge:
            case GroundType.BridgeLane: bridgePool.Release(target); break;
            // コンベヤー
            case GroundType.Conveyor: conveyorPool.Release(target); break;
        }
    }
}
