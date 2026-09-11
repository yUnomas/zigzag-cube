using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyParameterTable", menuName = "ZigzagCube/DifficultyParameterTable")]
public class DifficultyParameterTable : ScriptableObject
{
    [SerializeField] private DifficultyParameterEntry[] entries;

    public DifficultyParameterEntry GetEntry(int level)
    {
        if(entries == null) return default;

        // 現在の難易度レベルと合致するグループを取得
        foreach (var e in entries)
        {
            if (e.level == level) return e;
        }

        Debug.Log("現在の難易度レベルと合致するパラメーター設定が存在しません");
        return entries[0];
    }
}
