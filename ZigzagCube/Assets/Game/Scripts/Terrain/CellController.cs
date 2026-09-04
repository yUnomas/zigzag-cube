using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class CellController : MonoBehaviour
{
    private GroundPoolController groundPool;
    private GimmickPoolController gimmickPool;
    private LanePool lanePool;

    private GroundType activeGroundType;
    private StageObjectBase activeGround;
    private GimmickType activeGimmickType;
    private StageObjectBase activeGimmick;
    private Lane activeGroundLane;
    private Lane activeGimmickLane;

    private void Awake()
    {
        groundPool = FindAnyObjectByType<GroundPoolController>();
        gimmickPool = FindAnyObjectByType<GimmickPoolController>();
        lanePool = FindAnyObjectByType<LanePool>();
    }

    public void Clear()
    {
        groundPool.Release(activeGroundType, activeGround);
        gimmickPool.Release(activeGimmickType, activeGimmick);
        lanePool.Release(activeGroundLane);
        lanePool.Release(activeGimmickLane);

        activeGroundType = GroundType.None;
        activeGround = null;
        activeGimmickType = GimmickType.None;
        activeGimmick = null;
        activeGroundLane = null;
        activeGimmickLane = null;
}
    public void SetGround(GroundData data)
    {
        activeGroundType = data.type;   // 渡された地面タイプを保持
        if (data.type == GroundType.None) return;

        switch (data.type)
        {
            case GroundType.Ground:
                {
                    Ground ground = groundPool.Get(data.type) as Ground;
                    ground.Set(transform, data);
                    activeGround = ground;
                }
                break;
            case GroundType.Bridge:
                {
                    Bridge bridge = groundPool.Get(data.type) as Bridge;
                    bridge.Set(transform, data);
                    activeGround = bridge;
                }
                break;
            case GroundType.MovingBridge:
                {
                    Bridge bridge = groundPool.Get(data.type) as Bridge;
                    bridge.Set(transform, data);
                    activeGround = bridge;

                    Lane lane = lanePool.Get();
                    lane.Set(transform, bridge.transform, data.direction);
                    activeGroundLane = lane;
                }
                break;
            case GroundType.Conveyor:
                {
                    Conveyor conveyor = groundPool.Get(data.type) as Conveyor;
                    conveyor.Set(transform, data);
                    activeGround = conveyor;
                }
                break;
        }
    }
    public void SetGimmick(GimmickData data)
    {
        activeGimmickType = data.type;  // 渡されたギミックタイプを保持
        if(data.type == GimmickType.None) return;
        
        switch (data.type)
        {
            case GimmickType.Spike:
                {
                    Spike spike = gimmickPool.Get(data.type) as Spike;
                    spike.Set(transform, data);
                    activeGimmick = spike;
                }
                break;
            case GimmickType.SpikeLane:
                {
                    Spike spike = gimmickPool.Get(data.type) as Spike;
                    spike.Set(transform, data);
                    activeGimmick = spike;

                    Lane lane = lanePool.Get();
                    lane.Set(transform, spike.transform, data.direction);
                    activeGimmickLane = lane;
                }
                break;
            case GimmickType.Cannon:
                {
                    Cannon cannon = gimmickPool.Get(data.type) as Cannon;
                    cannon.Set(transform, data);
                    activeGimmick = cannon;
                }
                break;
        }
    }
}
