using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class CellView : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject obstacleRoot;
    [SerializeField] private GameObject obstaclePrefab;

    private List<GameObject> obstacleInstances = new List<GameObject>();

    public void Clear()
    {
        // 地面
        ground.SetActive(false);
        // 障害物
        foreach(var obstacle in obstacleInstances) { obstacle.SetActive(false); }
    }
    public void SetGround(int width)
    {
        // 地面の表示とサイズ設定
        ground.SetActive(true);
        Vector3 scale = ground.transform.localScale;
        scale.x = width;
        ground.transform.localScale = scale;
    }
    public void SetObstacle(List<ObstacleData> obstacles)
    {
        if(obstacles == null) return;
        for(int i = 0; i < obstacles.Count; i++)
        {
            // 障害物のインスタンスの不足分を生成
            if (obstacleInstances.Count <= i)
            {
                GameObject obj = Instantiate(obstaclePrefab);
                obj.transform.parent = obstacleRoot.transform;
                obstacleInstances.Add(obj);
            }
            // 障害物の表示と位置設定
            obstacleInstances[i].SetActive(true);
            obstacleInstances[i].transform.localPosition = new Vector3(obstacles[i].laneIndex, 1, 0);
        }
    }
}
