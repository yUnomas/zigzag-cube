using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    private GroundPoolController groundPool;
    private GimmickPoolController gimmickPool;
    private DecorationPoolController decorationPool;
    private LanePool lanePool;

    private List<GroundBase> activeGrounds = new List<GroundBase>();
    private List<GimmickBase> activeGimmicks = new List<GimmickBase>();
    private List<DecorationBase> activeDecorations = new List<DecorationBase>();
    private List<Lane> activeLanes = new List<Lane>();

    private void Awake()
    {
        groundPool = FindAnyObjectByType<GroundPoolController>();
        gimmickPool = FindAnyObjectByType<GimmickPoolController>();
        decorationPool = FindAnyObjectByType<DecorationPoolController>();
        lanePool = FindAnyObjectByType<LanePool>();
    }

    public void Clear()
    {
        // 使用中のオブジェクトをプールに返却
        foreach(GroundBase ground in activeGrounds)
        {
            groundPool.Release(ground.type, ground);
        }
        foreach(GimmickBase gimmick in activeGimmicks)
        {
            gimmickPool.Release(gimmick.type, gimmick);
        }
        foreach(Lane lane in activeLanes)
        {
            lanePool.Release(lane);
        }
        foreach (DecorationBase decoration in activeDecorations)
        {
            decorationPool.Release(decoration.type, decoration);
        }
        // 各リストをクリア
        activeGrounds.Clear();
        activeGimmicks.Clear();
        activeDecorations.Clear();
        activeLanes.Clear();
}
    public void SetGround(GroundData data, DifficultyParameterEntry param)
    {
        if (data.type == GroundType.None || data.isOccupied) return;
        // 地面タイプを参照してプールから取得
        GroundBase activeGround = null;
        Lane activeLane = null;
        switch (data.type)
        {
            case GroundType.Ground:
                {
                    activeGround = groundPool.Get(data.type) as Ground;
                }
                break;
            case GroundType.Bridge:
                {
                    activeGround = groundPool.Get(data.type) as Bridge;
                }
                break;
            case GroundType.MovingBridge:
                {
                    activeGround = groundPool.Get(data.type) as Bridge;
                    activeLane = lanePool.Get();
                }
                break;
            case GroundType.Conveyor:
                {
                    activeGround = groundPool.Get(data.type) as Conveyor;
                }
                break;
        }
        // 取得した地面・レーンを配置
        if (activeGround != null)
        {
            activeGround.Set(transform, data, param);
            activeGrounds.Add(activeGround);
        }
        if(activeLane != null)
        {
            activeLane.Set(transform, activeGround.transform, data.direction, param.movingBridgeSpeed);
            activeLanes.Add(activeLane);
        }
    }
    public void SetGimmick(GimmickData data, DifficultyParameterEntry param)
    {
        if(data.type == GimmickType.None) return;
        // ギミックタイプを参照してプールから取得
        GimmickBase activeGimmick = null;
        Lane activeLane = null;
        switch (data.type)
        {
            case GimmickType.Spike:
                {
                    activeGimmick = gimmickPool.Get(data.type) as Spike;
                }
                break;
            case GimmickType.SpikeLane:
                {
                    activeGimmick = gimmickPool.Get(data.type) as Spike;
                    activeLane = lanePool.Get();
                }
                break;
            case GimmickType.Cannon:
                {
                    activeGimmick = gimmickPool.Get(data.type) as Cannon;
                }
                break;
        }
        // 取得したギミック・レーンを配置
        if(activeGimmick != null)
        {
            activeGimmick.Set(transform, data, param);
            activeGimmicks.Add(activeGimmick);
        }
        if(activeLane != null)
        {
            activeLane.Set(transform, activeGimmick.transform, data.direction, param.movingBridgeSpeed);
            activeLanes.Add(activeLane);
        }
    }
    public void SetDecoration(List<DecorationData> decorations)
    {
        if (decorations == null || decorations.Count == 0) return;

        // 装飾を全て配置
        foreach (DecorationData data in decorations)
        {
            if (data.type == DecorationType.None) continue;
            // 装飾タイプを参照してプールから取得
            DecorationBase activeDecoration = null;
            switch (data.type)
            {
                case DecorationType.Grass: activeDecoration = decorationPool.Get(data.type) as Grass; break;
                case DecorationType.Flower: activeDecoration = decorationPool.Get(data.type) as Flower; break;
            }
            // 取得した装飾を配置
            if (activeDecoration != null)
            {
                activeDecoration.Set(transform, data);
                activeDecorations.Add(activeDecoration);
            }
        }
    }
}
