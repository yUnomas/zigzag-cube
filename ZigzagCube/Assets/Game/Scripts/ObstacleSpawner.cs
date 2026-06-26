using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField,Tooltip("地面のTransform情報(生成範囲として使用)")]
    private Transform groundTransform;
    [SerializeField, Tooltip("生成ごとの最大生成数")]
    private int maxSpawnCount = 7;
    [SerializeField, Tooltip("生成数の増加量")]
    private int spawnIncreaseAmount = 1;
    [SerializeField, Tooltip("生成数が増加するスコア間隔")]
    private float spawnIncreasePerScore = 500;
    [Header("=====")]
    [SerializeField] private GameObject obstaclePrefab;

    /// <summary>
    /// 最後に生成数が増加した際のスコア値   </summary>
    private float lastSpawnIncreaseScore;
    /// <summary>
    /// 生成回数    </summary>
    private int spawnCount = 1;
    /// <summary>
    /// 生成済み障害物    </summary>
    private List<GameObject> spawnedObstacles = new List<GameObject>();

    private void Update()
    {
        // 一定のスコア間隔で生成数を増加
        int currentScore = GameplayManager.Instance.Score;
        if (currentScore - lastSpawnIncreaseScore >= spawnIncreasePerScore)
        {
            spawnCount += spawnIncreaseAmount;
            spawnCount = Mathf.Min(spawnCount, maxSpawnCount);  // 最大数に抑える

            lastSpawnIncreaseScore = currentScore;
        }
    }

    /// <summary>
    /// 障害物の生成    </summary>
    public void Spawn()
    {
        HashSet<Vector3Int> usedSpawnPos = new HashSet<Vector3Int>();   // 使用済みの配置位置
        Vector3 spawnArea = groundTransform.localScale;

        // 生成回数に応じたループ
        for(int spawnIndex = 0; spawnIndex < spawnCount; spawnIndex++)
        {
            // 他の障害物と被らないように配置位置を取得
            Vector3Int spawnPos = new Vector3Int();
            while (!Input.GetKeyDown(KeyCode.Escape))
            {
                spawnPos = new Vector3Int(
                        (int)Random.Range((spawnArea.x - 2) / -2, (spawnArea.x - 2) / 2),
                        1,
                        (int)Random.Range(spawnArea.z / -2, spawnArea.z / 2)
                    );
                if (!usedSpawnPos.Contains(spawnPos)) break;
            }
            // 初回生成時はインスタンス生成と親設定
            if (spawnedObstacles.Count < spawnIndex + 1)
            {
                GameObject obj = Instantiate(obstaclePrefab, Vector3.zero, Quaternion.identity);
                spawnedObstacles.Add(obj);
                obj.transform.parent = transform;
            }
            // 生成位置に配置
            spawnedObstacles[spawnIndex].transform.localPosition = spawnPos;
        }
    }
}
