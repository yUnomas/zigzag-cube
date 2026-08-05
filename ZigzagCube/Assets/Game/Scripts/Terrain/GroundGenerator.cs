using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("初回生成の切り替え")]
    private bool isGenerateAtStart = true;

    /// <summary>
    /// 生成の有無    </summary>
    private bool isGenerate;

    private void Awake()
    {
        isGenerate = isGenerateAtStart;
    }

    public GroundData[] Generate(int width, int cellCount)
    {
        GroundData[] groundDatas = new GroundData[cellCount];
        if (!isGenerate)
        {
            for (int i = 0; i < groundDatas.Length; i++)
            {
                groundDatas[i] = new GroundData()
                {
                    type = GroundType.Default,
                    laneIndex = width / 2,
                    width = width
                };
            }
            return groundDatas;
        }

        for (int i = 0; i < groundDatas.Length; i++)
        {
            int rand = Random.Range(0, 10);
            if (rand < 9)
            {
                groundDatas[i] = new GroundData()
                {
                    type = GroundType.Default,
                    laneIndex = width / 2,
                    width = width
                };
            }
            else
            {
                int minWidth = (int)(width / 1.5);
                int randWidth = Random.Range(minWidth, width - minWidth);
                int minCenterIndex = (randWidth - 1) / 2;
                int maxCenterIndex = minCenterIndex + (width - randWidth);

                groundDatas[i] = new GroundData()
                {
                    type = GroundType.Narrow,
                    laneIndex = Random.Range(minCenterIndex, maxCenterIndex + 1),
                    width = randWidth
                };
            }
        }

        return groundDatas;
    }
}
