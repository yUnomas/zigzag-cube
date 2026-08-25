using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField] private Ground ground;
    [SerializeField] private Bridge bridge;
    [SerializeField] private Conveyor conveyor;
    [SerializeField] private Spike spike;
    [SerializeField] private SpikeLane spikeLane;
    [SerializeField] private Cannon cannon;

    public void Clear()
    {
        // 地面
        ground.Clear();
        bridge.Clear();
        spike.Clear();
        conveyor.Clear();
    }
    public void SetGround(GroundData data)
    {
        if (data.type == GroundType.None) return;

        switch (data.type)
        {
            case GroundType.Normal: ground.Set(data.startLaneIndex, data.width); break;
            case GroundType.Bridge: bridge.Set(data.startLaneIndex, data.width, data.direction, false); break;
            case GroundType.MovingBridge: bridge.Set(data.startLaneIndex, data.width, data.direction, true); break;
            case GroundType.BridgeLane: bridge.SetWithLane(data.startLaneIndex, data.width, data.direction); break;
            case GroundType.Conveyor: conveyor.Set(data.startLaneIndex, data.width, data.direction); break;
        }
    }
    public void SetGimmick(GimmickData data)
    {
        if(data.type == GimmickType.None) return;
        
        switch (data.type)
        {
            case GimmickType.Spike: spike.Set(data.laneIndex, data.width); break;
            case GimmickType.SpikeLane: spikeLane.Set(data.laneIndex, data.width, data.direction);  break;
            case GimmickType.Cannon: cannon.Set(data.laneIndex, data.width); break;
        }
    }
}
