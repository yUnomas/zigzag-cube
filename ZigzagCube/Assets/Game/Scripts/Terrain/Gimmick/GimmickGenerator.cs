using System;
using UnityEngine;

public class GimmickGenerator : MonoBehaviour
{
    [SerializeField] private int generateCount = 2;

    private GimmickType GetType(GroundType groundType)
    {
        switch(groundType)
        {
            case GroundType.None: return GimmickType.None;
            case GroundType.Bridge:
            case GroundType.MovingBridge:
            case GroundType.BridgeLane: return (GimmickType)UnityEngine.Random.Range((int)GimmickType.Spike, (int)GimmickType.Cannon);
            default:    return (GimmickType)UnityEngine.Random.Range((int)GimmickType.Spike, (int)GimmickType.Max);
        }
    }
    private GimmickData CreateData(GimmickType type, int startLaneIndex, int width, int direction = 1)
    {
        return new GimmickData()
        {
            type = type,
            laneIndex = startLaneIndex,
            y = 1,
            width = width,
            direction = direction
        };
    }

    public GimmickData[] Generate(ChunkType chunkType, GroundData[] groundDatas)
    {
        if (chunkType <= ChunkType.Start) return Array.Empty<GimmickData>();

        // 生成する数の配列作成
        GimmickData[] gimmickDatas = new GimmickData[groundDatas.Length];

        for (int i = 0; i < generateCount; i++)
        {
            // ギミックの配置セルの決定
            int cellIndex;
            while (true)
            {
                cellIndex = UnityEngine.Random.Range(0, groundDatas.Length);
                if (gimmickDatas[cellIndex].type == GimmickType.None) break;
            }
            // 配置ギミックデータの作成
            GroundData ground = groundDatas[cellIndex];
            GimmickType type = GetType(ground.type);
            int laneIndex = UnityEngine.Random.Range(ground.startLaneIndex, ground.startLaneIndex + ground.width);
            switch (type)
            {
                case GimmickType.Spike:
                case GimmickType.Cannon:
                    {
                        gimmickDatas[cellIndex] = CreateData(type, laneIndex, 1);
                    }
                    break;
                case GimmickType.SpikeLane:
                    {
                        int direction = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
                        gimmickDatas[cellIndex] = CreateData(type, laneIndex, 1, direction);
                    }
                    break;
            }
        }

        return gimmickDatas;
    }
}
