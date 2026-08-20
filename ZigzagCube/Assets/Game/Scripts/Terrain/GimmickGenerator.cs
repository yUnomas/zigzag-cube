using System;
using UnityEngine;

public class GimmickGenerator : MonoBehaviour
{
    [SerializeField] private int generateCount = 2;

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
            GimmickType type = (GimmickType)UnityEngine.Random.Range((int)GimmickType.Spike, (int)GimmickType.Max);
            GroundData ground = groundDatas[cellIndex];
            switch (type)
            {
                case GimmickType.Spike:
                    {
                        int laneIndex = UnityEngine.
                            Random.Range(ground.startLaneIndex, ground.startLaneIndex + ground.width);

                        gimmickDatas[cellIndex] = new GimmickData()
                        {
                            type = type,
                            laneIndex = laneIndex,
                            width = 1,
                        };
                    }
                    break;
            }
        }

        return gimmickDatas;
    }
}
