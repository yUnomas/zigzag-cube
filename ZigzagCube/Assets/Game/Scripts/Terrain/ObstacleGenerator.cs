using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("初回の生成数")]
    private int startGenerateCount;
    [SerializeField, Tooltip("生成ごとの最大生成数")]
    private int maxGenerateCount = 7;
    [SerializeField, Tooltip("生成数の増加量")]
    private int generateIncreaseAmount = 1;
    [SerializeField, Tooltip("生成数が増加するスコア間隔")]
    private float generateIncreasePerScore = 500;

    /// <summary>
    /// 最後に生成数が増加した際のスコア値   </summary>
    private float lastGenerateIncreaseScore;
    /// <summary>
    /// 生成回数    </summary>
    private int generateCount;

    private void Awake()
    {
        generateCount = startGenerateCount;
    }
    private void Update()
    {
        // 一定のスコア間隔で生成数を増加
        int currentScore = GameplayManager.Instance.Score;
        if (currentScore - lastGenerateIncreaseScore >= generateIncreasePerScore)
        {
            generateCount += generateIncreaseAmount;
            generateCount = Mathf.Min(generateCount, maxGenerateCount);  // 最大数に抑える

            lastGenerateIncreaseScore = currentScore;
            generateIncreasePerScore += generateIncreasePerScore;

            Debug.Log("チャンク毎の障害物数が増加");
        }
    }

    /// <summary>
    /// 障害物の生成    </summary>
    public ObstacleData[] Generate(ChunkType chunkType, GroundData[] groundDatas)
    {
        if(chunkType == ChunkType.Start) return Array.Empty<ObstacleData>();

        // 生成する数分の配列作成
        ObstacleData[] obstacleDatas = new ObstacleData[generateCount];
        HashSet<Vector3Int> usedGeneratePosition = new HashSet<Vector3Int>();
        for (int i = 0; i < generateCount; i++)
        {
            // 他の障害物と被らないように生成情報を作成
            Vector3Int pos;
            while(true)
            {
                int cellIndex = UnityEngine.Random.Range(0, groundDatas.Length);
                int laneIndex = UnityEngine.Random.Range(
                    groundDatas[cellIndex].startLaneIndex,
                    groundDatas[cellIndex].startLaneIndex + groundDatas[cellIndex].width);

                pos = new Vector3Int(laneIndex, 1, cellIndex);

                if (!usedGeneratePosition.Contains(pos)) break;
            }
            // 生成情報を保存
            obstacleDatas[i] = new ObstacleData()
            {
                type = ObstacleType.Rock,
                cellIndex = pos.z,
                laneIndex = pos.x,
            };
        }
        return obstacleDatas;
    }
}