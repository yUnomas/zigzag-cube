using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private List<ChunkController> chunks = new List<ChunkController>();
    [SerializeField] private Water water;
    [SerializeField] private GameObject editorGuideObjects;
    [Header("Systems")]
    [SerializeField] private PlayerController player;
    [SerializeField] private DifficultyManager difficultyManager;

    private void Awake()
    {
        if(editorGuideObjects) Destroy(editorGuideObjects);
    }
    private void Start()
    {
        Generate();
    }
    private void LateUpdate()
    {
        CheckChunk();
    }

    /// <summary>
    /// フィールド生成    </summary>
    private void Generate()
    {
        // 各チャンクを生成
        foreach (var chunk in chunks)
        {
            chunk.Regenerate(false, chunks.Count, difficultyManager);
        }
    }
    /// <summary>
    /// チャンクを再生成するか確認   </summary>
    private void CheckChunk()
    {
        // プレイヤーから一定以上離れたら再生成
        foreach (var chunk in chunks)
        {
            if (player.transform.position.z - chunk.transform.position.z >= chunk.Length * 2)
            {
                chunk.Regenerate(true, chunks.Count, difficultyManager);
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
                revivePoint = chunk.RevivePoint;
            }
        }

        // 安全なチャンクに再生成
        reviveChunk?.Regenerate(false, chunks.Count, difficultyManager, ChunkType.Start);
        return revivePoint;
    }
}
