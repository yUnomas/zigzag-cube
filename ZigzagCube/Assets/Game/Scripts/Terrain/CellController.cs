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
        activeGroundType = data.type;
        if (data.type == GroundType.None) return;

        switch (data.type)
        {
            case GroundType.Normal:
                {
                    Ground ground = groundPool.Get(data.type) as Ground;
                    ground.Set(transform, data.startLaneIndex, data.y, data.width);
                    activeGround = ground;
                }
                break;
            case GroundType.Bridge:
                {
                    Bridge bridge = groundPool.Get(data.type) as Bridge;
                    bridge.Set(transform, data.startLaneIndex, data.y, data.width);
                    activeGround = bridge;
                }
                break;
            case GroundType.MovingBridge:
                {
                    Bridge bridge = groundPool.Get(data.type) as Bridge;
                    bridge.Set(transform, data.startLaneIndex, data.y, data.width);
                    activeGround = bridge;

                    Lane lane = lanePool.Get();
                    lane.Set(transform, 5, data.y, 1, bridge.gameObject, data.direction, false);
                    activeGroundLane = lane;
                }
                break;
            case GroundType.BridgeLane:
                {
                    Bridge bridge = groundPool.Get(data.type) as Bridge;
                    bridge.Set(transform, data.startLaneIndex, data.y, data.width);
                    activeGround = bridge;

                    Lane lane = lanePool.Get();
                    lane.Set(transform, 5, data.y, 1, bridge.gameObject, data.direction, true);
                    activeGroundLane = lane;
                }
                break;
            case GroundType.Conveyor:
                {
                    Conveyor conveyor = groundPool.Get(data.type) as Conveyor;
                    conveyor.Set(transform, data.startLaneIndex, data.y, data.width, data.direction);
                    activeGround = conveyor;
                }
                break;
        }
    }
    public void SetGimmick(GimmickData data)
    {
        activeGimmickType = data.type;
        if(data.type == GimmickType.None) return;
        
        switch (data.type)
        {
            case GimmickType.Spike:
                {
                    Spike spike = gimmickPool.Get(data.type) as Spike;
                    spike.Set(transform, data.laneIndex, data.y, data.width);
                    activeGimmick = spike;
                }
                break;
            case GimmickType.SpikeLane:
                {
                    Spike spike = gimmickPool.Get(data.type) as Spike;
                    spike.Set(transform, data.laneIndex, data.y, data.width);
                    activeGimmick = spike;

                    Lane lane = lanePool.Get();
                    lane.Set(transform, 5, data.y, 1, spike.gameObject, data.direction, true);
                    activeGimmickLane = lane;
                }
                break;
            case GimmickType.Cannon:
                {
                    Cannon cannon = gimmickPool.Get(data.type) as Cannon;
                    cannon.Set(transform, data.laneIndex, data.width, data.width);
                    activeGimmick = cannon;
                }
                break;
        }
    }
}
