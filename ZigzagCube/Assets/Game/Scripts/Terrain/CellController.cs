using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private RockView rockView;

    public void Clear()
    {
        // 地面
        ground.SetActive(false);
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
            switch (obstacles[i].type)
            {
                case ObstacleType.Rock:
                    rockView.Set(obstacles[i].laneIndex);
                    break;
            }
        }
    }
}
