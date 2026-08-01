using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [SerializeField, Tooltip("横方向の長さ")]
    private int width;
    public int Width => width;
    [SerializeField, Tooltip("進行方向への長さ")]
    private int length;
    public int Length => length;
    [Header("=====")]
    [SerializeField] private List<CellView> cells;
    [SerializeField] ObstacleSpawner obstacleSpawner;

    private void Start()
    {
        GenerateCell();
    }
    /// <summary>
    /// チャンクの再生成    </summary>
    public void Regenerate(int chunkCount)
    {
        LoopPosition(chunkCount);
        GenerateCell();

        Debug.Log("再生成が完了しました");
    }
    /// <summary>
    /// チャンクを最後尾へ移動    </summary>
    private void LoopPosition(int chunkCount)
    {
        transform.position += Vector3.forward * length * chunkCount;
    }
    private void GenerateCell()
    {
        // 障害物の生成位置を取得
        Vector3[] obstaclePositions = obstacleSpawner.Generate();

        //** 各セルのオブジェクト設定
        for(int index = 0; index < cells.Count; index++)
        {
            cells[index].Clear();
            cells[index].SetGround(width);
            // 障害物の生成位置と合致するセルに障害物を生成
            if(obstaclePositions != null)
            {
                foreach (var obstaclePos in obstaclePositions)
                {
                    if (obstaclePos.z != index) continue;
                    cells[index].SetObstacle(obstaclePos);
                }
            }
        }
    }
}