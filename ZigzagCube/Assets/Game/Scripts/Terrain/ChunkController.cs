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
    [SerializeField] GimmickGenerator gimmickGenerator;
    [SerializeField] private ChunkType type;

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

        if(isGenerate)
        {
            type = (ChunkType)Random.Range((int)ChunkType.Normal, (int)ChunkType.Conveyor + 1);
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
        GroundData[] groundDatas = groundGenerator.Generate(type, width, cells.Length);
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
        //** 各セルのオブジェクト設定
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].Clear();

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
    /// <summary>
    /// 復活地点の取得    </summary>
    public Vector3Int GetRevivePoint()
    {
        return new Vector3Int
            (
                (int)transform.position.x + width / 2,
                (int)transform.position.y + 1,
                (int)transform.position.z + 1
            );
    }
}