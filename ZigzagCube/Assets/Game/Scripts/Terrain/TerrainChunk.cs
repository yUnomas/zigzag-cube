using UnityEngine;

public class TerrainChunk : MonoBehaviour
{
    [SerializeField, Tooltip("チャンクの一辺の長さ")]
    private float length;
    public float Length => length;
    [Header("=====")]
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private WallSpawner wallSpawner;

    private void Start()
    {
        obstacleSpawner.Spawn();
        wallSpawner.Spawn();
    }
    /// <summary>
    /// チャンクの再生成    </summary>
    public void Regenerate(int chunkCount)
    {
        LoopPosition(chunkCount);
        obstacleSpawner.Spawn();
        wallSpawner.Spawn();
    }
    /// <summary>
    /// チャンクを最後尾へ移動    </summary>
    public void LoopPosition(int chunkCount)
    {
        transform.position += Vector3.forward * length * chunkCount;
    }
}
