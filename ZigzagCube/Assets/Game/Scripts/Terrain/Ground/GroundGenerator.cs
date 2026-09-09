using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    private GroundData CreateGround(GroundType type, int width, int length)
    {
        return new GroundData()
        {
            type = type,
            startLane = 0,
            width = width,
            length = length,
            height = 0,
        };
    }
    private GroundData CreateBridge(GroundType type, int startLane, int width, int length)
    {
        return new GroundData()
        {
            type = type,
            startLane = startLane,
            width = width,
            length = length,
            height = 0,
            direction = Random.Range(0, 2) == 0 ? 1 : -1
        };
    }
    private GroundData CreateConveyor(GroundType type, int width, int length)
    {
        return new GroundData()
        {
            type = type,
            startLane = 0,
            width = width,
            length = length,
            height = 0,
            direction = Random.Range(0, 2) == 0 ? 1 : -1
        };
    }

    public GroundData[] Generate(ChunkType chunkType, int chunkWidth, int chunkLength, int totalCells)
    {
        // セル数の地面データ作成
        GroundData[] groundDatas = new GroundData[totalCells];
        //** 地面データの生成処理
        switch (chunkType)
        {
            case ChunkType.Start:
            case ChunkType.Normal:
                break;
            case ChunkType.Bridge:
            case ChunkType.MovingBridge:
                {
                    // X軸方向の幅
                    int minWidth = (int)(chunkWidth / 2);
                    int maxWidth = (int)(chunkWidth / 1.5);
                    int randWidth = Random.Range(minWidth, maxWidth + 1);
                    int startLane = Random.Range(1, chunkWidth - randWidth + 1);
                    // Z軸方向の長さ
                    int startCell = Random.Range(1, chunkLength / 2);
                    int endCell = Random.Range(startCell + 1, chunkLength - 1);
                    int length = endCell - startCell + 1;
                    // 橋の種類
                    GroundType type = chunkType == ChunkType.Bridge
                        ? GroundType.Bridge
                        : GroundType.MovingBridge;
                    // データに橋の情報を適用
                    for (int i = startCell; i <= endCell; i++)
                    {
                        if (i == startCell)
                        {
                            groundDatas[i] = CreateBridge(type, startLane, randWidth, length);
                        }
                        else
                        {
                            groundDatas[i] = CreateBridge(GroundType.Occupied, startLane, randWidth, length);
                        }
                    }
                }
                break;
            case ChunkType.Conveyor:
                {
                    // 一定範囲のセルにコンベヤーを生成
                    int startCell = Random.Range(1, chunkLength / 2);
                    int endCell = Random.Range(startCell + 1, chunkLength - 1);
                    int length = endCell - startCell + 1;

                    for (int i = startCell; i <= endCell; i++)
                    {
                        if (i == startCell)
                        {
                            groundDatas[i] = CreateConveyor(GroundType.Conveyor, chunkWidth, length);
                        }
                        else
                        {
                            groundDatas[i] = CreateConveyor(GroundType.Occupied, chunkWidth, length);
                        }
                    }
                }
                break;
        }
        // 空データを整理
        for (int i = 0; i < totalCells; i++)
        {
            if(groundDatas[i].type == GroundType.None)
            {
                // 空データの連続数を取得
                int length = 0;
                while(i + length < totalCells && groundDatas[i + length].type == GroundType.None)
                {
                    length++;
                }
                // 空データの先頭セルに地面データを割り当て
                groundDatas[i] = CreateGround(GroundType.Ground, chunkWidth, length);
                // 以降を占有タイプに変更
                for (int j = 1; j < length; j++)
                {
                    groundDatas[i + j] = CreateGround(GroundType.Occupied, chunkWidth, length);
                }
                // 処理した分だけインクリメント
                i += length;
            }
        }

        return groundDatas;
    }
}