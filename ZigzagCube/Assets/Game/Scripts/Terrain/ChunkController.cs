using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;

public class ChunkController : MonoBehaviour
{
    [SerializeField, Tooltip("初回生成の切り替え")]
    private bool isGenerateAtStart = true;
    [SerializeField, Tooltip("横方向の長さ")]
    private int width;
    public int Width => width;
    [SerializeField, Tooltip("進行方向への長さ")]
    private int length;
    public int Length => length;
    [Header("=====")]
    [SerializeField] private CellController[] cells;
    [SerializeField] GroundGenerator groundGenerator;
    [SerializeField] ObstacleGenerator obstacleGenerator;
    [SerializeField] private ChunkType type;

    private bool isGenerate;

    private void Awake()
    {
        isGenerate = isGenerateAtStart;
    }
    private void Start()
    {
        CellData[] datas = Generate();
        Apply(datas);
    }

    private ChunkType GetChunkType()
    {
        if(isGenerate)
        {
            return (ChunkType)Random.Range((int)ChunkType.Normal, (int)ChunkType.Bridge);
        }
        else
        {
            isGenerate = true;
            return ChunkType.Normal;
        }
    }

    /// <summary>
    /// チャンクを最後尾へ移動    </summary>
    private void LoopPosition(int chunkCount)
    {
        transform.position += Vector3.forward * length * chunkCount;
    }
    /// <summary>
    /// チャンクの生成    </summary>
    private CellData[] Generate()
    {
        // 今回のチャンク種類を取得
        type = GetChunkType();
        // 各データの生成
        CellData[] cellDatas = new CellData[cells.Length];
        GroundData[] groundDatas = groundGenerator.Generate(type, width, cells.Length);
        ObstacleData[] obstacleDatas = obstacleGenerator.Generate(type, groundDatas);

        // 地面データを各セルに追加
        for(int i = 0; i < cellDatas.Length; i++)
        {
            cellDatas[i].ground = groundDatas[i];
        }
        // 障害物データを配置セルに追加
        foreach (ObstacleData data in obstacleDatas)
        {
            cellDatas[data.cellIndex].obstacles ??= new List<ObstacleData>();
            cellDatas[data.cellIndex].obstacles.Add(data);
        }

        return cellDatas;
    }
    /// <summary>
    /// チャンクの適用    </summary>
    private void Apply(CellData[] data)
    {
        //** 各セルのオブジェクト設定
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].Clear();

            cells[i].SetGround(data[i].ground);
            cells[i].SetObstacle(data[i].obstacles);
        }
    }

    /// <summary>
    /// チャンクの再生成    </summary>
    public void Regenerate(int chunkCount)
    {
        LoopPosition(chunkCount);
        CellData[] datas = Generate();
        Apply(datas);

        Debug.Log("再生成が完了しました");
    }
}