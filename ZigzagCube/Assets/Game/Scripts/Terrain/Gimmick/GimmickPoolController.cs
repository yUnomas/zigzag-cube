using UnityEngine;

public class GimmickPoolController : MonoBehaviour
{
    [SerializeField] private StageObjectPool spikePool;
    [SerializeField] private StageObjectPool cannonPool;

    public StageObjectBase Get(GimmickType type)
    {
        switch (type)
        {
            // トゲ
            case GimmickType.Spike:
            case GimmickType.SpikeLane: return spikePool.Get();
            // 大砲
            case GimmickType.Cannon: return cannonPool.Get();

            default: return null;
        }
    }
    public void Release(GimmickType type, StageObjectBase target)
    {
        if(type == GimmickType.None || target == null) return;

        switch (type)
        {
            // トゲ
            case GimmickType.Spike:
            case GimmickType.SpikeLane: spikePool.Release(target); break;
            // 大砲
            case GimmickType.Cannon: cannonPool.Release(target); break;
        }
    }
}
