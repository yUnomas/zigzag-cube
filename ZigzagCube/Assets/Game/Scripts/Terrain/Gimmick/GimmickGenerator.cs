using UnityEngine;

public class GimmickGenerator : MonoBehaviour
{
    [SerializeField] private GimmickGenerateTable normalTable;
    [SerializeField] private GimmickGenerateTable movingTable;

    private DifficultyManager difficulty => DifficultyManager.Instance;

    private GimmickType GetRandomGimmick(GroundType type)
    {
        switch(type)
        {
            // 動かない地面
            case GroundType.Ground:
            case GroundType.Bridge: return normalTable.GetRandomGimmick(difficulty.CurrentLevel);
            // 動く地面
            case GroundType.MovingBridge:
            case GroundType.Conveyor: return movingTable.GetRandomGimmick(difficulty.CurrentLevel);
            // それ以外
            default: return default;
        }
    }
    private int GetGenerateCount(ChunkType type)
    {
        switch (type)
        {
            // 動く地面のないチャンク
            case ChunkType.Normal:
            case ChunkType.Bridge: return normalTable.GetGenerateCount(difficulty.CurrentLevel);
            // 動く地面のあるチャンク
            case ChunkType.MovingBridge:
            case ChunkType.Conveyor: return movingTable.GetGenerateCount(difficulty.CurrentLevel);
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

    public GimmickData[] Generate(ChunkType chunkType, int totalCells, GroundData[] groundDatas)
    {
        if (chunkType <= ChunkType.Start) return default;

        // 合計セル数の配列作成
        GimmickData[] gimmickDatas = new GimmickData[totalCells];
        int generateCount = GetGenerateCount(chunkType);
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
            GimmickType gimmickType = GetRandomGimmick(ground.type);
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
