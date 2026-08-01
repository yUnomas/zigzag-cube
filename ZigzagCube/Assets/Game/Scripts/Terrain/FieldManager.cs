using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField]
    private List<ChunkController> chunks = new List<ChunkController>();
    
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
                chunk.Regenerate(chunks.Count);
            }
        }
    }
}
