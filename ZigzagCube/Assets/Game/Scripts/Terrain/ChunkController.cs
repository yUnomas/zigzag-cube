using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VoxelBusters.CoreLibrary;

public class ChunkController : MonoBehaviour
{
    [SerializeField, Tooltip("初回生成の切り替え")]
    private bool isGenerateAtStart = true;
    [SerializeField, Tooltip("X軸方向のレーン数")]
    private int width;
    public int Width => width;
    [SerializeField, Tooltip("Z軸方向のセル数")]
    private int length;
    public int Length => length;
    [Header("=====")]
    [SerializeField] private CellController[] cells;
    [SerializeField] GroundGenerator groundGenerator;
    [SerializeField] GimmickGenerator gimmickGenerator;
    [SerializeField] private ChunkType type;

    /// <summary>
    /// 復活地点    </summary>
    public Vector3Int RevivePoint => new Vector3Int(
        (int) transform.position.x + width / 2,
        (int) transform.position.y + 1,
        (int) transform.position.z + 1
        );
    /// <summary>
    /// 生成の有無    </summary>
    private bool isGenerate;

    private void Awake()
    {
        isGenerate = isGenerateAtStart;
    }
    private void Start()
    {
        Regenerate(false);
    }

    /// <summary>
    /// 生成するチャンクタイプを設定    </summary>
    private void SetChunkType(ChunkType chunkType = ChunkType.None)
    {
        // チャンクタイプが渡されている場合は渡された値を設定
        if(chunkType != ChunkType.None)
        {
            type = chunkType;
            return;
        }

        // 生成の有無によって生成方式を変更
        if(isGenerate)
        {
            type = (ChunkType)Random.Range((int)ChunkType.Normal, (int)ChunkType.Max);
        }
        else
        {
            isGenerate = true;
            type = ChunkType.Start;
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
        // 各データの生成
        CellData[] cellDatas = new CellData[cells.Length];
        GroundData[] groundDatas = groundGenerator.Generate(type, width, length, cells.Length);
        GimmickData[] gimmickDatas = gimmickGenerator.Generate(type, groundDatas);
        // 各データをセルに追加
        for(int i = 0; i < cellDatas.Length; i++)
        {
            if(!groundDatas.IsNullOrEmpty()) cellDatas[i].ground = groundDatas[i];
            if(!gimmickDatas.IsNullOrEmpty()) cellDatas[i].gimmick = gimmickDatas[i];
        }

        return cellDatas;
    }
    /// <summary>
    /// チャンクの適用    </summary>
    private void Apply(CellData[] data)
    {
        // 各セルのオブジェクト設定
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].Clear();   // 前回の要素をあらかじめ除外
            cells[i].SetGround(data[i].ground);
            cells[i].SetGimmick(data[i].gimmick);
        }
    }

    /// <summary>
    /// チャンクの再生成    </summary>
    public void Regenerate(bool isLoop, int chunkCount = 0, ChunkType chunkType = ChunkType.None)
    {
        if(isLoop)  LoopPosition(chunkCount);

        SetChunkType(chunkType);
        CellData[] datas = Generate();
        Apply(datas);

        Debug.Log("再生成が完了しました");
    }
}