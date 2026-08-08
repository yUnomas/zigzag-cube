using System;
using UnityEngine;

public class GimmickGenerator : MonoBehaviour
{
    public GimmickData[] Generate(ChunkType chunkType, int chunkWidth, GroundData[] groundDatas)
    {
        if (chunkType < ChunkType.Conveyor) return Array.Empty<GimmickData>();

        // 生成する数の配列作成
        GimmickData[] gimmickDatas = Array.Empty<GimmickData>();

        switch (chunkType)
        {
            case ChunkType.Conveyor:
                {
                    int startCellIndex = UnityEngine.Random.Range(0, chunkWidth / 2);
                    int endCellIndex = UnityEngine.Random.Range(startCellIndex + 1, chunkWidth - 2);
                    gimmickDatas = new GimmickData[endCellIndex - startCellIndex + 1];

                    for (int i = 0; i < gimmickDatas.Length; i++)
                    {
                        gimmickDatas[i] = new GimmickData()
                        {
                            type = GimmickType.Conveyor,
                            cellIndex = startCellIndex + i,
                            direction = UnityEngine.Random.Range(0, 2) == 0 ? new Vector2Int(1, 0) : new Vector2Int(-1, 0)
                        };
                    }
                }
                break;
        }

        return gimmickDatas;
    }
}
