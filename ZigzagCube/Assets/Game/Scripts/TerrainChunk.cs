using UnityEngine;

public class TerrainChunk : MonoBehaviour
{
    [SerializeField, Tooltip("チャンクの一辺の長さ")]
    private float length;
    public float Length => length;
    [Header("=====")]
    [SerializeField] private ObstacleSpawner obstacleSpawner;

    private void Start()
    {
        obstacleSpawner.Spawn();
    }
    /// <summary>
    /// チャンクの再生成    </summary>
    public void Regenerate(int chunkCount)
    {
        LoopPosition(chunkCount);
        obstacleSpawner.Spawn();
    }
    /// <summary>
    /// チャンクを最後尾へ移動    </summary>
    public void LoopPosition(int chunkCount)
    {
        transform.position += Vector3.forward * length * chunkCount;
    }
}
