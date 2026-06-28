using System.Collections.Generic;
using UnityEngine;

public static class CollectionUtility
{
    /// <summary>
    /// リスト内の要素をシャッフル    </summary>
    public static void Shuffle<T>(List<T> list)
    {
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
    }
}
