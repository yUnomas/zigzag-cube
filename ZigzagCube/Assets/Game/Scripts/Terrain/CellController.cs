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
    public void SetGround(GroundData data)
    {
        float x = data.startLaneIndex + (float)(data.width - 1) / 2;
        // 地面の表示とサイズ設定
        ground.SetActive(true);
        ground.transform.localPosition = new Vector3(x, 0, 0);
        ground.transform.localScale = new Vector3(data.width, 1, 1);
    }
    public void SetObstacle(List<ObstacleData> datas)
    {
        if(datas == null) return;
        for(int i = 0; i < datas.Count; i++)
        {
            switch (datas[i].type)
            {
                case ObstacleType.Rock:
                    rockView.Set(datas[i].laneIndex);
                    break;
            }
        }
    }
}
