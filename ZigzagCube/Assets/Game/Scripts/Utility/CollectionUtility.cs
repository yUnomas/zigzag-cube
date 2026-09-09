using System.Collections.Generic;
using UnityEngine;

public static class CollectionUtility
{
    /// <summary>
    /// リスト内の要素をシャッフル    </summary>
    public static List<T> Shuffle<T>(List<T> list)
    {
        if(list == null || list.Count == 0)
        {
            Debug.LogWarning($"{list}の中身が空またはNullです");
            return default;
        }

        // リストの要素数のループ
        for(int index = 0; index < list.Count; index++)
        {
            // リスト範囲内で乱数生成
            int randNum = Random.Range(0, list.Count - index);
            // 現在/乱数の要素番号にある要素の配置入れ替え
            T tmp = list[index];
            list[index] = list[randNum];
            list[randNum] = tmp;
        }
        return list;
    }

    /// <summary>
    /// 重み付きリストの中からランダム抽選    </summary>
    public static T Choose<T>(IReadOnlyList<T> list) where T : IWeighted
    {
        if (list == null || list.Count == 0)
        {
            Debug.LogWarning($"{list}の中身が空またはNullです");
            return default;
        }

        // 重みの合計を計算
        int totalWeight = 0;
        foreach (var item in list)
        {
            totalWeight += item.Weight;
        }
        // 閾値を参照してランダム抽選
        int randNum = Random.Range(1, totalWeight + 1);
        int threshold = 0;
        foreach (var item in list)
        {
            threshold += item.Weight;
            if (randNum <= threshold) return item;
        }

        return list[0];
    }
}
