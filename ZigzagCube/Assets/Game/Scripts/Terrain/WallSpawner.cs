using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("最大減少数")]
    private int maxDecreaseCount = 20;
    [SerializeField, Tooltip("生成数の減少量")]
    private int spawnDecreaseAmount = 1;
    [SerializeField, Tooltip("生成数が減少するスコア間隔")]
    private float spawnDecreasePerScore = 500;
    [Header("=====")]
    [SerializeField] private List<GameObject> walls;

    /// <summary>
    /// 最後に生成数が増加した際のスコア値   </summary>
    private float lastSpawnDecreaseScore;
    /// <summary>
    /// 減少回数    </summary>
    private int decreaseCount = 0;
    /// <summary>
    /// 非表示の壁    </summary>
    private List<GameObject> invisibleWalls = new List<GameObject>();

    private void Update()
    {
        // 一定のスコア間隔で生成数を増加
        int currentScore = GameplayManager.Instance.Score;
        if (currentScore - lastSpawnDecreaseScore >= spawnDecreasePerScore)
        {
            decreaseCount += spawnDecreaseAmount;
            decreaseCount = Mathf.Min(decreaseCount, maxDecreaseCount);  // 最大数に抑える

            lastSpawnDecreaseScore = currentScore;
        }
    }

    /// <summary>
    /// 障害物の生成    </summary>
    public void Spawn()
    {
        // 非表示させる必要がなければ早期リターン
        if (decreaseCount == 0) return;

        // 壁のリセット
        foreach(GameObject wall in invisibleWalls) { wall.SetActive(true); }
        invisibleWalls.Clear();
        // 壁リストをシャッフル
        CollectionUtility.Shuffle(walls);
        // 壁リストを減少回数に応じて非表示に切り替え
        for(int invisibleCount = 0; invisibleCount < decreaseCount; invisibleCount++)
        {
            walls[invisibleCount].SetActive(false);
            invisibleWalls.Add(walls[invisibleCount]);
        }
    }
}
