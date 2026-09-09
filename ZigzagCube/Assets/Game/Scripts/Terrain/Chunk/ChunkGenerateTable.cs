using UnityEngine;

[CreateAssetMenu(fileName = "ChunkGenerateTable", menuName = "ZigzagCube/ChunkGenerateTable")]
public class ChunkGenerateTable : ScriptableObject
{
    [SerializeField] private LevelChunkGroup[] groups;

    public ChunkType GetRandomChunk(int level)
    {
        // 現在の難易度レベルと合致したグループを取得
        LevelChunkGroup group = groups[0];
        foreach(var g in groups)
        {
            if(group.entries == null || group.entries.Length == 0)
            {
                continue;
            }
            if (level == g.level)
            {
                group = g;  break;
            }
        }
        // グループの中からチャンクを選択
        ChunkWeightEntry selectedEntry = CollectionUtility.Choose(group.entries);
        return selectedEntry.type;
    }
}