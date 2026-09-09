using UnityEngine;

[CreateAssetMenu(fileName = "GimmickGenerateTable", menuName = "ZigzagCube/GimmickGenerateTable")]
public class GimmickGenerateTable : ScriptableObject
{
    [SerializeField] private LevelGimmickGroup[] groups;

    public int GetGenerateCount(int level)
    {
        // 現在の難易度レベルと合致するグループを取得
        LevelGimmickGroup group = groups[0];
        foreach (var g in groups)
        {
            if (group.entries == null || group.entries.Length == 0)
            {
                continue;
            }
            if (level == g.level)
            {
                group = g; break;
            }
        }
        return group.generateCount;
    }
    public GimmickType GetRandomGimmick(int level)
    {
        // 現在の難易度レベルと合致するグループを取得
        LevelGimmickGroup group = groups[0];
        foreach (var g in groups)
        {
            if (group.entries == null || group.entries.Length == 0)
            {
                continue;
            }
            if (level == g.level)
            {
                group = g; break;
            }
        }
        // グループの中からギミックをランダム抽選
        GimmickWeightEntry selectedEntry = CollectionUtility.Choose(group.entries);
        return selectedEntry.type;
    }
}