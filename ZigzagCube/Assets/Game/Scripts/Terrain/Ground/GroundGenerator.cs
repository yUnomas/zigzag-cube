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
    private GroundData CreateBridge(GroundType type, int startLane, int width, int length, int direction)
    {
        return new GroundData()
        {
            type = type,
            startLane = startLane,
            width = width,
            length = length,
            height = 0,
            direction = direction
        };
    }
    private GroundData CreateConveyor(GroundType type, int width, int length, int direction)
    {
        return new GroundData()
        {
            type = type,
            startLane = 0,
            width = width,
            length = length,
            height = 0,
            direction = direction
        };
    }

    public GroundData[] Generate(ChunkType chunkType, int chunkWidth, int chunkLength, int cellCount)
    {
        // セル数の地面データ作成
        GroundData[] groundDatas = new GroundData[cellCount];
        //** 地面データの生成処理
        switch (chunkType)
        {
            case ChunkType.Start:
            case ChunkType.Normal:
                break;
            case ChunkType.Bridge:
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

                    // 確率で動く橋に変更
                    int rand = Random.Range(0, 10);
                    int direction = 0;
                    GroundType type = GroundType.Bridge;
                    if (rand < 3)
                    {
                        direction = Random.Range(0, 2) == 0 ? 1 : -1;
                        type = GroundType.MovingBridge;

                    }
                    // データに橋の情報を適用
                    for (int i = startCell; i <= endCell; i++)
                    {
                        if (i == startCell)
                        {
                            groundDatas[i] = CreateBridge(type, startLane, randWidth, length, direction);
                        }
                        else
                        {
                            groundDatas[i] = CreateBridge(GroundType.Occupied, startLane, randWidth, length, direction);
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
                    int direction = Random.Range(0, 2) == 0 ? 1 : -1;

                    for (int i = startCell; i <= endCell; i++)
                    {
                        if (i == startCell)
                        {
                            groundDatas[i] = CreateConveyor(GroundType.Conveyor, chunkWidth, length, direction);
                        }
                        else
                        {
                            groundDatas[i] = CreateConveyor(GroundType.Occupied, chunkWidth, length, direction);
                        }
                    }
                }
                break;
        }
        // 空データを整理
        for (int i = 0; i < cellCount; i++)
        {
            if(groundDatas[i].type == GroundType.None)
            {
                // 空データの連続数を取得
                int length = 0;
                while(i + length < cellCount && groundDatas[i + length].type == GroundType.None)
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