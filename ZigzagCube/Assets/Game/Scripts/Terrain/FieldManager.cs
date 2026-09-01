using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] private List<ChunkController> chunks = new List<ChunkController>();
    [SerializeField] private Water water;
    
    private PlayerController player;

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
            if (player.transform.position.z - chunk.transform.position.z >= chunk.Length * 2)
            {
                chunk.Regenerate(true, chunks.Count);
                water.transform.position += Vector3.forward * chunk.Length;
            }
        }
    }

    /// <summary>
    /// プレイヤーが復活するチャンクの準備    </summary>
    /// <returns>
    /// 復活地点   </returns>
    public Vector3Int PrepareRevivePoint()
    {
        // プレイヤー座標よりも手前にある最も近いチャンクを取得
        ChunkController reviveChunk = null;
        Vector3Int revivePoint = Vector3Int.one;
        foreach(var chunk in chunks)
        {
            if(chunk.transform.position.z < player.transform.position.z &&
                player.transform.position.z - revivePoint.z > player.transform.position.z - chunk.transform.position.z)
            {
                reviveChunk = chunk;
                revivePoint = chunk.GetRevivePoint();
            }
        }

        // 安全なチャンクに再生成
        reviveChunk?.Regenerate(false, chunks.Count, ChunkType.Start);
        return revivePoint;
    }
}
