using System;
using System.Linq;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("初回生成の切り替え")]
    private bool isGenerateAtStart = true;
    [SerializeField, Tooltip("初回の生成数")]
    private int startGenerateCount;
    [SerializeField, Tooltip("生成ごとの最大生成数")]
    private int maxGenerateCount = 7;
    [SerializeField, Tooltip("生成数の増加量")]
    private int generateIncreaseAmount = 1;
    [SerializeField, Tooltip("生成数が増加するスコア間隔")]
    private float generateIncreasePerScore = 500;

    /// <summary>
    /// 生成の有効化    </summary>
    private bool isGenerate;
    /// <summary>
    /// 最後に生成数が増加した際のスコア値   </summary>
    private float lastGenerateIncreaseScore;
    /// <summary>
    /// 生成回数    </summary>
    private int generateCount;

    private void Awake()
    {
        isGenerate = isGenerateAtStart;
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
    public ObstacleData[] Generate(int width, int length)
    {
        if(!isGenerate)
        {
            isGenerate = true;
            return Array.Empty<ObstacleData>();
        }

        // 生成する数分の配列作成
        ObstacleData[] generatedData = new ObstacleData[generateCount];
        for (int i = 0; i < generateCount; i++)
        {
            // 他の障害物と被らないように生成情報を作成
            ObstacleData data = new ObstacleData();
            while (!Input.GetKeyDown(KeyCode.Escape))
            {
                data.cellIndex = UnityEngine.Random.Range(0, length);
                data.laneIndex = UnityEngine.Random.Range(width / -2, width / 2);

                if (!generatedData.Contains(data)) break;
            }
            // 生成情報を保存
            generatedData[i] = data;
        }
        return generatedData;
    }
}