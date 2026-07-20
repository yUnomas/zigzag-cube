using System.Collections.Generic;
using UnityEngine;

public class TerrainChunkManager : MonoBehaviour
{
    [SerializeField, Tooltip("各チャンクのTerrainChunk情報")]
    private List<TerrainChunk> chunks = new List<TerrainChunk>();

    PlayerController player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
    }
    private void LateUpdate()
    {
        CheckChunk();
    }

    /// <summary>
    /// チャンク確認   </summary>
    private void CheckChunk()
    {
        // プレイヤーから一定以上離れたら再生成
        foreach (var chunk in chunks)
        {
            if (player.transform.position.z - chunk.transform.position.z >= chunk.Length)
            {
                chunk.Regenerate(chunks.Count);
            }
        }
    }
}
