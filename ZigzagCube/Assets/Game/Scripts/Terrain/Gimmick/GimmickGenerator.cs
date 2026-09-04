using System;
using UnityEngine;

public class GimmickGenerator : MonoBehaviour
{
    [SerializeField] private int generateCount = 2;

    private GimmickType GetType(GroundType groundType)
    {
        switch(groundType)
        {
            // 何も生成しない
            case GroundType.None: return GimmickType.None;
            // トゲ or レーン移動トゲ
            case GroundType.Bridge:
            case GroundType.MovingBridge: return (GimmickType)UnityEngine.Random.Range((int)GimmickType.Spike, (int)GimmickType.Cannon);
            // トゲ or レーン移動トゲ or 大砲
            default:    return (GimmickType)UnityEngine.Random.Range((int)GimmickType.Spike, (int)GimmickType.Max);
        }
    }
    private GimmickData CreateData(GimmickType type, int lane, int direction = 1)
    {
        return new GimmickData()
        {
            type = type,
            lane = lane,
            height = 1,
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
            int cell;
            while (true)
            {
                cell = UnityEngine.Random.Range(0, groundDatas.Length);
                if (gimmickDatas[cell].type == GimmickType.None) break;
            }
            // 配置ギミックデータの作成
            GroundData ground = groundDatas[cell];
            GimmickType type = GetType(ground.type);
            int lane = UnityEngine.Random.Range(ground.startLane, ground.startLane + ground.width);
            switch (type)
            {
                case GimmickType.Spike:
                case GimmickType.Cannon:
                    {
                        gimmickDatas[cell] = CreateData(type, lane);
                    }
                    break;
                case GimmickType.SpikeLane:
                    {
                        int direction = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
                        gimmickDatas[cell] = CreateData(type, lane, direction);
                    }
                    break;
            }
        }

        return gimmickDatas;
    }
}
