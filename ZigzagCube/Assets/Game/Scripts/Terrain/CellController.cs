using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField] private Ground ground;
    [SerializeField] private Bridge bridge;
    [SerializeField] private ObstacleView spikeCubeView;
    [SerializeField] private Conveyor conveyor;

    public void Clear()
    {
        // 地面
        ground.Clear();
        bridge.Clear();
        spikeCubeView.Clear();
        conveyor.Clear();
    }
    public void SetGround(GroundData data)
    {
        if (data.type == GroundType.None) return;

        switch (data.type)
        {
            case GroundType.Normal: ground.View(data.startLaneIndex, data.width); break;
            case GroundType.Bridge: bridge.View(data.startLaneIndex, data.width); break;
        }
    }
    public void SetObstacle(List<ObstacleData> datas)
    {
        if(datas == null) return;
        for(int i = 0; i < datas.Count; i++)
        {
            switch (datas[i].type)
            {
                case ObstacleType.SpikeCube:
                    spikeCubeView.Set(datas[i].laneIndex);
                    break;
            }
        }
    }
    public void SetGimmick(GimmickData data)
    {
        if (data.type == GimmickType.None) return;

        switch (data.type)
        {
            case GimmickType.Conveyor:
                {
                    conveyor.Set(data.width, data.direction.x);
                }
                break;
        }
    }
}
