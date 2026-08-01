using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("初回生成の切り替え")]
    private bool isSpawnAtStart;
    [SerializeField, Tooltip("初回の生成数")]
    private int startSpawnCount;
    [SerializeField,Tooltip("生成範囲")]
    private Vector3Int spawnArea;
    [SerializeField, Tooltip("生成ごとの最大生成数")]
    private int maxSpawnCount = 7;
    [SerializeField, Tooltip("生成数の増加量")]
    private int spawnIncreaseAmount = 1;
    [SerializeField, Tooltip("生成数が増加するスコア間隔")]
    private float spawnIncreasePerScore = 500;

    /// <summary>
    /// 生成の有効化    </summary>
    private bool isSpawn;
    /// <summary>
    /// 最後に生成数が増加した際のスコア値   </summary>
    private float lastSpawnIncreaseScore;
    /// <summary>
    /// 生成回数    </summary>
    private int spawnCount;

    private void Awake()
    {
        isSpawn = isSpawnAtStart;
        spawnCount = startSpawnCount;
    }
    private void Update()
    {
        // 一定のスコア間隔で生成数を増加
        int currentScore = GameplayManager.Instance.Score;
        if (currentScore - lastSpawnIncreaseScore >= spawnIncreasePerScore)
        {
            spawnCount += spawnIncreaseAmount;
            spawnCount = Mathf.Min(spawnCount, maxSpawnCount);  // 最大数に抑える

            lastSpawnIncreaseScore = currentScore;
            spawnIncreasePerScore += spawnIncreasePerScore;

            Debug.Log("チャンク毎の障害物数が増加");
        }
    }

    /// <summary>
    /// 障害物の生成    </summary>
    public Vector3[] Generate()
    {
        Vector3[] generateData = new Vector3[spawnCount];

        // 生成が無効化されている場合の早期リターン
        if(!isSpawn)
        {
            isSpawn = true;
            return null;
        }

        // 初期値の取得
        HashSet<Vector3Int> usedSpawnPos = new HashSet<Vector3Int>();   // 使用済みの配置位置
        // 生成回数に応じたループ
        for(int spawnIndex = 0; spawnIndex < spawnCount; spawnIndex++)
        {
            // 他の障害物と被らないように配置位置を取得
            Vector3Int spawnPos = new Vector3Int();
            while (!Input.GetKeyDown(KeyCode.Escape))
            {
                spawnPos = new Vector3Int(
                        (int)Random.Range(spawnArea.x / -2, spawnArea.x / 2),
                        (int)spawnArea.y,
                        (int)Random.Range(0, spawnArea.z)
                    );
                if (!usedSpawnPos.Contains(spawnPos)) break;
            }
            // 生成位置を保存
            generateData[spawnIndex] = spawnPos;
            usedSpawnPos.Add(spawnPos);
        }
        return generateData;
    }
}
