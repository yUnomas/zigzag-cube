using UnityEditor.ShaderGraph;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    [SerializeField] 

    public GroundData[] Generate(ChunkType chunkType, int chunkWidth, int cellCount)
    {
        // セル数の地面データ作成
        GroundData[] groundDatas = new GroundData[cellCount];

        //** 地面データの生成処理
        switch (chunkType)
        {
            case ChunkType.Start:
            case ChunkType.Normal:
                for(int i = 0; i < cellCount; i++)
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

                    int startCellIndex = Random.Range(0, chunkWidth / 2);
                    int endCellIndex = Random.Range(startCellIndex + 1, chunkWidth - 1);

                    for (int i = 0; i < cellCount; i++)
                    {
                        if(i >= startCellIndex && i <= endCellIndex)
                        {
                            groundDatas[i] = CreateData(GroundType.Bridge, startLaneIndex, randWidth);
                        }
                        else
                        {
                            groundDatas[i] = CreateData(GroundType.Normal, 0, chunkWidth);
                        }
                    }
                }
                break;
            case ChunkType.Conveyor:
                for (int i = 0; i < cellCount; i++)
                {
                    groundDatas[i] = CreateConveyor(chunkWidth);
                }
                break;
        }

        return groundDatas;
    }

    private GroundData CreateData(GroundType type, int startLaneIndex, int width)
    {
        return new GroundData()
        {
            type = type,
            startLaneIndex = startLaneIndex,
            width = width
        };
    }
    private GroundData CreateConveyor(int chunkWidth)
    {
        GroundData groundData = new GroundData();
        return groundData;
    }
}