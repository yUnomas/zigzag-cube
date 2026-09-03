using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    private GroundData CreateData(GroundType type, int startLaneIndex, int width, int direction = 1)
    {
        return new GroundData()
        {
            type = type,
            startLaneIndex = startLaneIndex,
            y = 0,
            width = width,
            direction = direction
        };
    }

    public GroundData[] Generate(ChunkType chunkType, int chunkWidth, int cellCount)
    {
        // セル数の地面データ作成
        GroundData[] groundDatas = new GroundData[cellCount];

        //** 地面データの生成処理
        switch (chunkType)
        {
            case ChunkType.Start:
            case ChunkType.Normal:
                for (int i = 0; i < cellCount; i++)
                {
                    groundDatas[i] = CreateData(GroundType.Normal, 0, chunkWidth);
                }
                break;
            case ChunkType.Bridge:
                {
                    int minWidth = (int)(chunkWidth / 2);
                    int maxWidth = (int)(chunkWidth / 1.5);
                    int randWidth = Random.Range(minWidth, maxWidth + 1);
                    int startLaneIndex = Random.Range(1, chunkWidth - randWidth + 1);

                    int startCellIndex = Random.Range(1, chunkWidth / 2);
                    int endCellIndex = Random.Range(startCellIndex + 1, chunkWidth - 1);

                    int rand = Random.Range(0, 10);
                    if(rand < 3)
                    {
                        int direction = Random.Range(0, 2) == 0 ? 1 : -1;
                        int bridgeLength = endCellIndex - startCellIndex + 1;
                        int laneCellIndex = startCellIndex + bridgeLength / 2;

                        for (int i = 0; i < cellCount; i++)
                        {
                            if (i == laneCellIndex)
                            {
                                groundDatas[i] = CreateData(GroundType.BridgeLane, startLaneIndex, randWidth, direction);
                            }
                            else if (i >= startCellIndex && i <= endCellIndex)
                            {
                                groundDatas[i] = CreateData(GroundType.MovingBridge, startLaneIndex, randWidth, direction);
                            }
                            else
                            {
                                groundDatas[i] = CreateData(GroundType.Normal, 0, chunkWidth);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < cellCount; i++)
                        {
                            if (i >= startCellIndex && i <= endCellIndex)
                            {
                                groundDatas[i] = CreateData(GroundType.Bridge, startLaneIndex, randWidth);
                            }
                            else
                            {
                                groundDatas[i] = CreateData(GroundType.Normal, 0, chunkWidth);
                            }
                        }
                    }
                }
                break;
            case ChunkType.Conveyor:
                {
                    // 一定範囲のセルにコンベヤーを生成
                    int startCellIndex = Random.Range(1, chunkWidth / 2);
                    int endCellIndex = Random.Range(startCellIndex + 1, chunkWidth - 2);
                    int direction = Random.Range(0, 2) == 0 ? 1 : -1;

                    for (int i = 0; i < cellCount; i++)
                    {
                        if (i >= startCellIndex && i <= endCellIndex)
                        {
                            groundDatas[i] = CreateData(GroundType.Conveyor, 0, chunkWidth, direction);
                        }
                        else
                        {
                            groundDatas[i] = CreateData(GroundType.Normal, 0, chunkWidth);
                        }
                    }
                }
                break;
        }

        return groundDatas;
    }
}