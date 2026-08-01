using System.Collections.Generic;
using UnityEngine;

public class CellView : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject obstacleRoot;
    [SerializeField] private GameObject obstaclePrefab;

    private List<GameObject> obstacleInstances = new List<GameObject>();
    private int obstacleGeneratedCount; // 障害物の生成回数

    public void Clear()
    {
        // 地面
        ground.SetActive(false);
        // 障害物
        foreach(var obstacle in obstacleInstances) { obstacle.SetActive(false); }
        obstacleGeneratedCount = 0;
    }
    public void SetGround(int width)
    {
        // 地面の表示とサイズ設定
        ground.SetActive(true);
        Vector3 scale = ground.transform.localScale;
        scale.x = width;
        ground.transform.localScale = scale;
    }
    public void SetObstacle(Vector3 setPosition)
    {
        // 障害物のインスタンスの不足分を生成
        if (obstacleInstances.Count <= obstacleGeneratedCount)
        {
            GameObject obj = Instantiate(obstaclePrefab);
            obj.transform.parent = obstacleRoot.transform;
            obstacleInstances.Add(obj);
        }
        // 障害物の表示と位置設定
        obstacleInstances[obstacleGeneratedCount].SetActive(true);
        obstacleInstances[obstacleGeneratedCount].transform.localPosition = new Vector3(setPosition.x, setPosition.y, 0);
        obstacleGeneratedCount++;   // 生成回数を保存
    }
}
