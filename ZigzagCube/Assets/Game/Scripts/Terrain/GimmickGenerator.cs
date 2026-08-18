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
                    // 一定範囲のセルにコンベヤーを生成
                    int startCellIndex = UnityEngine.Random.Range(1, chunkWidth / 2);
                    int endCellIndex = UnityEngine.Random.Range(startCellIndex + 1, chunkWidth - 2);
                    gimmickDatas = new GimmickData[endCellIndex - startCellIndex + 1];

                    for (int i = 0; i < gimmickDatas.Length; i++)
                    {
                        // コンベヤーデータの作成
                        gimmickDatas[i] = new GimmickData()
                        {
                            type = GimmickType.Conveyor,
                            cellIndex = startCellIndex + i,
                            width = chunkWidth,
                            direction = UnityEngine.Random.Range(0, 2) == 0 ? new Vector2Int(1, 0) : new Vector2Int(-1, 0)
                        };
                        // コンベヤーを配置する地面のデータ情報を変更
                        groundDatas[gimmickDatas[i].cellIndex].type = GroundType.None;
                    }
                }
                break;
        }

        return gimmickDatas;
    }
}
