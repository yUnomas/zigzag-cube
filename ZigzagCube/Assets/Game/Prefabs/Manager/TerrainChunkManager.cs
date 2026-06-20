using System.Collections.Generic;
using UnityEngine;

public class TerrainChunkManager : MonoBehaviour
{
    [SerializeField, Tooltip("チャンクの一辺の長さ")]
    private float chunkLength;
    [SerializeField, Tooltip("各チャンクのTransform情報")]
    private List<Transform> chunks = new List<Transform>();

    PlayerController player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
    }
    private void LateUpdate()
    {
        LoopChunk();
    }

    /// <summary>
    /// プレイヤーがチャンクから十分離れたら、チャンクを前方に移動   </summary>
    private void LoopChunk()
    {
        foreach (var chunk in chunks)
        {
            if (player.transform.position.z - chunk.transform.position.z >= chunkLength * 2)
            {
                chunk.transform.position += Vector3.forward * chunkLength * chunks.Count;
            }
        }
    }
}
