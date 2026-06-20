using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField,Tooltip("生成範囲")]
    private Vector3 spawnArea;
    [Header("=====")]
    [SerializeField] private GameObject obstaclePrefab;

    /// <summary>
    /// 生成済み障害物    </summary>
    private GameObject generatedObstacle;

    /// <summary>
    /// 障害物の生成    </summary>
    public void Generate()
    {
        // スポーン範囲内からランダムに生成位置を取得
        Vector3 generatePos = new Vector3(
                Random.Range(spawnArea.x / -2, spawnArea.x / 2),
                1f,
                Random.Range(spawnArea.x / -2, spawnArea.x / 2)
            );
        // 初回生成時はインスタンス生成と親設定
        if(generatedObstacle == null)
        {
            generatedObstacle = Instantiate(obstaclePrefab, Vector3.zero, Quaternion.identity);
            generatedObstacle.transform.parent = transform;
        }
        // 生成位置に配置
        generatedObstacle.transform.localPosition = generatePos;
    }
}
