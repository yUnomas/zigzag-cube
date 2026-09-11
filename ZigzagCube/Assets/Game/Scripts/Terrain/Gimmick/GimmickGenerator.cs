using UnityEngine;

public class GimmickGenerator : MonoBehaviour
{
    [SerializeField] private GimmickGenerateTable normalTable;
    [SerializeField] private GimmickGenerateTable movingTable;

    private GimmickType GetRandomGimmick(int difficultyLevel, GroundType type)
    {
        switch(type)
        {
            // 動かない地面
            case GroundType.Ground:
            case GroundType.Bridge: return normalTable.GetRandomGimmick(difficultyLevel);
            // 動く地面
            case GroundType.MovingBridge:
            case GroundType.Conveyor: return movingTable.GetRandomGimmick(difficultyLevel);
            // それ以外
            default: return default;
        }
    }
    private int GetGenerateCount(int difficultyLevel, ChunkType type)
    {
        switch (type)
        {
            // フルサイズ
            case ChunkType.Normal: return normalTable.GetGenerateCount(difficultyLevel);
            // 狭い
            case ChunkType.Bridge: return normalTable.GetGenerateCount(difficultyLevel) - 1;
            // 移動＋狭い
            case ChunkType.MovingBridge: return movingTable.GetGenerateCount(difficultyLevel) - 1;
            // 移動
            case ChunkType.Conveyor: return movingTable.GetGenerateCount(difficultyLevel);
            // それ以外
            default: return default;
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

    public GimmickData[] Generate(int difficultyLevel, ChunkType chunkType, int totalCells, GroundData[] groundDatas)
    {
        if (chunkType <= ChunkType.Start) return default;

        // 合計セル数の配列作成
        GimmickData[] gimmickDatas = new GimmickData[totalCells];
        int generateCount = GetGenerateCount(difficultyLevel, chunkType);
        for (int i = 0; i < generateCount; i++)
        {
            // ギミックを配置するセルの決定
            int cell;
            while (true)
            {
                cell = Random.Range(0, totalCells);
                if (gimmickDatas[cell].type == GimmickType.None) break;
            }
            // ギミックデータの作成
            GroundData ground = groundDatas[cell];
            GimmickType gimmickType = GetRandomGimmick(difficultyLevel, ground.type);
            int lane = Random.Range(ground.startLane, ground.startLane + ground.width);
            switch (gimmickType)
            {
                case GimmickType.Spike:
                case GimmickType.Cannon:
                    {
                        gimmickDatas[cell] = CreateData(gimmickType, lane);
                    }
                    break;
                case GimmickType.SpikeLane:
                    {
                        int direction = Random.Range(0, 2) == 0 ? 1 : -1;
                        gimmickDatas[cell] = CreateData(gimmickType, lane, direction);
                    }
                    break;
            }
        }

        return gimmickDatas;
    }
}
