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

                }
                break;
        }

        return gimmickDatas;
    }
}
